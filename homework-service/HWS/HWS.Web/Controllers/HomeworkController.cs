using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWS.Controllers.DTOs.Requests;
using HWS.Controllers.DTOs.Responses;
using HWS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HWS.Controllers
{
    // ToDo: JWT
    [Route("api/homeworks")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;
        private readonly IHomeworkService homeworkService;

        public HomeworkController(IGroupService groupService, IUserService userService, IHomeworkService homeworkService)
        {
            this.groupService = groupService;
            this.userService = userService;
            this.homeworkService = homeworkService;
        }

        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetHomeworkComments(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateHomeworkComment(Guid id, [FromBody] CommentRequest request)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ApplyToHomework(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
