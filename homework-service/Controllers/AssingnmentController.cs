using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework_service.Controllers.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace homework_service.Controllers
{
    // ToDo: JWT
    [Route("api/assignments")]
    [ApiController]
    public class AssingnmentController : ControllerBase
    {
        public AssingnmentController()
        {

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AssignmentResponse>>> GetAssignments()
        {
            return Ok(new List<AssignmentResponse>());
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
