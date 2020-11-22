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
    [Route("api/homeworks")]
    [ApiController]
    [Authorize]
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

        private User getUser() => (User)HttpContext?.Items["User"];

        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetHomeworkComments(Guid id)
        {
            var user = getUser();

            var homework = await homeworkService.GetHomework(id).ConfigureAwait(false);

            if (homework == null)
                throw new HWSException("Homework not found", StatusCodes.Status404NotFound);

            if (!homeworkService.UserIsAppliedToHomework(user, homework))
                throw new HWSException("User is not applied to the homework", StatusCodes.Status403Forbidden);

            var commentsResponse = homework.Comments.Select(comment => new CommentResponse(comment));

            return Ok(commentsResponse);
        }

        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponse>> CreateHomeworkComment(Guid id, [FromBody] CommentRequest request)
        {
            var user = getUser();

            if (request == null)
                throw new HWSException("Request body cannot be null", StatusCodes.Status400BadRequest);

            var homework = await homeworkService.GetHomework(id).ConfigureAwait(false);

            if (homework == null)
                throw new HWSException("Homework not found", StatusCodes.Status404NotFound);

            if (!homeworkService.UserIsAppliedToHomework(user, homework))
                throw new HWSException("User is not applied to the homework", StatusCodes.Status403Forbidden);

            var savedHomeworkComment = await homeworkService.CreateComment(user, homework, request.Content).ConfigureAwait(false);

            return Created(nameof(CreateHomeworkComment), new CommentResponse(savedHomeworkComment));
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AssignmentResponse>> ApplyToHomework(Guid id)
        {
            var user = getUser();

            var homework = await homeworkService.GetHomework(id).ConfigureAwait(false);

            if (homework == null)
                throw new HWSException("Homework not found", StatusCodes.Status404NotFound);

            var group = await groupService.GetGroup(homework.Group.Id).ConfigureAwait(false);

            if (group == null)
                throw new HWSException("Group not found", StatusCodes.Status500InternalServerError);

            try
            {
                var newAssignment = await homeworkService.CreateAssignment(user, group, homework).ConfigureAwait(false);

                return Created(nameof(ApplyToHomework), AssignmentResponse.Of(newAssignment));
            }
            catch (IllegalStudentException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            catch (HomeworkIsFullException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status409Conflict);
            }
        }
    }
}
