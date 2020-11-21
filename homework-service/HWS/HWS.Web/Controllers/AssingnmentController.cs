using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWS.Controllers.DTOs.Responses;
using HWS.Domain;
using HWS.Middlewares.Config;
using HWS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GradeAssignment(Guid id, [FromBody] string grade)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}/reserve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ReserveAssignment(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}/free")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> FreeAssignment(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
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
