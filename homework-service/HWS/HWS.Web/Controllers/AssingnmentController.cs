using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HWS.Controllers.DTOs.Requests;
using HWS.Controllers.DTOs.Responses;
using HWS.Domain;
using HWS.Middlewares.Config;
using HWS.Services;
using HWS.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HWS.Middlewares.ErrorHandlerMiddleware;

namespace HWS.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    [Authorize]
    public class AssingnmentController : ControllerBase
    {
        private readonly IAssignmentService assignmentService;
        private readonly IHomeworkService homeworkService;

        public AssingnmentController(IAssignmentService assignmentService, IHomeworkService homeworkService)
        {
            this.assignmentService = assignmentService;
            this.homeworkService = homeworkService;
        }

        private User getUser() => (User)HttpContext?.Items["User"];

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AssignmentResponse>>> GetAssignments()
        {
            var user = getUser();

            if (user == null)
                return Ok(new List<Assignment>().Select(AssignmentResponse.Of));

            switch (user.Role)
            {
                case Domain.User.UserRole.Student:
                    var assigmentsForStudent = await assignmentService.GetAssignmentsForStudent(user).ConfigureAwait(false);

                    return Ok(assigmentsForStudent.Select(AssignmentResponse.Of));

                case Domain.User.UserRole.Teacher:
                    var assigmentsForTeacher = await assignmentService.GetAssignmentsForTeacher(user).ConfigureAwait(false);

                    return Ok(assigmentsForTeacher.Select(AssignmentResponse.Of));

                case Domain.User.UserRole.Unknown:
                default:
                    return Ok(new List<Assignment>().Select(AssignmentResponse.Of));
            }
        }

        [HttpPost("{id}/grade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> GradeAssignment(Guid id, [FromBody] GradeRequest request)
        {
            var user = getUser();

            if (request == null)
                throw new HWSException("request cannot be null", StatusCodes.Status400BadRequest);

            if (user.Role != Domain.User.UserRole.Teacher)
                throw new HWSException("User has to be a teacher to grade an assignment", StatusCodes.Status403Forbidden);

            var assignment = await assignmentService.GetAssignment(id).ConfigureAwait(false);

            if (assignment == null)
                throw new HWSException("Assignment not found", StatusCodes.Status404NotFound);

            try
            {
                var success = await assignmentService.GradeAssignment(user, assignment, request.Grade).ConfigureAwait(false);
                if (success)
                    return Ok();
                else return Conflict();
            }
            catch (IllegalTeacherException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            catch (AssignmentNotReservedException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status409Conflict);
            }
            catch (AssignmentNotFreeException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status409Conflict);
            }
        }

        [HttpPost("{id}/reserve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> ReserveAssignment(Guid id)
        {
            var user = getUser();

            if (user.Role != Domain.User.UserRole.Teacher)
                throw new HWSException("User has to be a teacher to grade an assignment", StatusCodes.Status403Forbidden);

            var assignment = await assignmentService.GetAssignment(id).ConfigureAwait(false);

            if (assignment == null)
                throw new HWSException("Assignment not found", StatusCodes.Status404NotFound);

            try
            {
                var success = await assignmentService.ReserveAssignment(user, assignment).ConfigureAwait(false);
                if (success)
                    return Ok();
                else return Conflict();
            }
            catch (IllegalTeacherException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            catch (AssignmentNotFreeException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status409Conflict);
            }
        }

        [HttpPost("{id}/free")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> FreeAssignment(Guid id)
        {
            var user = getUser();

            if (user.Role != Domain.User.UserRole.Teacher)
                throw new HWSException("User has to be a teacher to grade an assignment", StatusCodes.Status403Forbidden);

            var assignment = await assignmentService.GetAssignment(id).ConfigureAwait(false);

            if (assignment == null)
                throw new HWSException("Assignment not found", StatusCodes.Status404NotFound);

            try
            {
                var success = await assignmentService.FreeAssignment(user, assignment).ConfigureAwait(false);
                if (success)
                    return Ok();
                else return Conflict();
            }
            catch (IllegalTeacherException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            catch (AssignmentNotFreeException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status409Conflict);
            }
        }

        [HttpPost("{id}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> ChangeAssignmentFile(Guid id, [FromForm] FileModel file)
        {
            var user = getUser();

            if (user.Role != Domain.User.UserRole.Student)
                throw new HWSException("User has to be a student to upload an assignment", StatusCodes.Status403Forbidden);

            var assignment = await assignmentService.GetAssignment(id).ConfigureAwait(false);

            if (assignment == null)
                throw new HWSException("Assignment not found", StatusCodes.Status404NotFound);

            if (assignment.Student.Id != user.Id)
                throw new HWSException("User is not allowed to upload this assignment", StatusCodes.Status403Forbidden);

            var homework = await homeworkService.GetHomework(assignment.Homework.Id).ConfigureAwait(false);

            if (file.FormFile.Length > homework.MaxFileSize * 1024 * 1024)
                throw new HWSException("File is too big", StatusCodes.Status400BadRequest);

            var type = '.' + file.FileName.Split('.').Last();

            if (!getMimeTypes().ContainsKey(type))
                throw new HWSException("File's type is not allowed", StatusCodes.Status400BadRequest);

            try
            {
                var newFileName = await assignmentService.ChangeAssignmentFile(assignment, file.FileName, file.FormFile);

                if (newFileName != null)
                    return Ok(newFileName);
                else
                    return Conflict();
            }
            catch (FileHandlingFailedException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status507InsufficientStorage);
            }
        }

        [HttpGet("{id}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAssignmentFile(Guid id)
        {
            var user = getUser();

            var assignment = await assignmentService.GetAssignment(id).ConfigureAwait(false);

            if (assignment == null)
                throw new HWSException("Assignment not found", StatusCodes.Status404NotFound);

            var homework = await homeworkService.GetHomework(assignment.Homework.Id).ConfigureAwait(false);

            if (assignment.Student.Id != user.Id && homework.Graders.FirstOrDefault(g => g.Id == user.Id) == null)
                throw new HWSException("User is not allowed to download this assignment", StatusCodes.Status403Forbidden);

            if (assignment.FileName == "")
                throw new HWSException("File not found", StatusCodes.Status404NotFound);

            try
            {
                var memory = await assignmentService.GetFile(assignment);
                return File(memory, getContentType(assignment.FileName), assignment.FileName);
            } 
            catch (Exception e)
            {
                throw new HWSException(e.Message, StatusCodes.Status507InsufficientStorage);
            } 
        }

        private string getContentType(string path)
        {
            var types = getMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> getMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
