using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWS.Controllers.DTOs.Requests;
using HWS.Controllers.DTOs.Responses;
using HWS.Domain;
using HWS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HWS.Controllers
{
    // ToDo: JWT
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;

        public GroupController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> GetGroups()
        {
            return Ok(new List<Group>().Select(GroupResponse.ForTeacher));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> CreateGroup([FromBody] GroupRequest request)
        {
            if (request == null)
                return BadRequest();

            var newGroup = request.ToNew();

            var id = Guid.Parse("5ebbec4b-a5da-484e-84e5-644c3118898a");

            var owner = await userService.GetUser(id);
            newGroup.Owner = owner;

            IReadOnlyCollection<User> students = await userService.GetUsers(request.students).ConfigureAwait(false);
            IReadOnlyCollection<User> teachers = await userService.GetUsers(request.teachers).ConfigureAwait(false);

            newGroup.Students = students.ToList();
            newGroup.Teachers = teachers.ToList();

            var savedGroup = await groupService.CreateGroup(newGroup);

            return Created(nameof(CreateGroup), GroupResponse.ForTeacher(savedGroup));
        }


        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetGroupComments(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateGroupComment(Guid id, [FromBody] CommentRequest request)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> JoinGroup(Guid id, [FromBody] string code)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("{id}/homework")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateHomework(Guid id, [FromBody] HomeworkRequest request)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
