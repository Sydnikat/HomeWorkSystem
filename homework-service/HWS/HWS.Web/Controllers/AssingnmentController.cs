using System;
using System.Collections.Generic;
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

        public AssingnmentController(IAssignmentService assignmentService)
        {
            this.assignmentService = assignmentService;
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

        [HttpGet("{id}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<byte[]>> GetAssignmentFile(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeAssignmentFile(Guid id, [FromBody] byte[] newFile)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
