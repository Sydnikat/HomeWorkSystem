using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWS.Controllers.DTOs.Requests;
using HWS.Controllers.DTOs.Responses;
using HWS.Domain;
using HWS.Middlewares.Config;
using HWS.Services;
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

            if (!userIsAppliedToHomework(user, homework))
                throw new HWSException("User is not applied to the homework", StatusCodes.Status403Forbidden);

            return Ok(homework.Comments);
        }

        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateHomeworkComment(Guid id, [FromBody] CommentRequest request)
        {
            var user = getUser();

            if (request == null)
                throw new HWSException("Request body cannot be null", StatusCodes.Status400BadRequest);

            var homework = await homeworkService.GetHomework(id).ConfigureAwait(false);

            if (homework == null)
                throw new HWSException("Homework not found", StatusCodes.Status404NotFound);

            if (!userIsAppliedToHomework(user, homework))
                throw new HWSException("User is not applied to the homework", StatusCodes.Status403Forbidden);

            var savedHomeworkComment = await homeworkService.CreateComment(user, homework, request.Content).ConfigureAwait(false);

            return Created(nameof(CreateHomeworkComment), new CommentResponse(savedHomeworkComment));
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ApplyToHomework(Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        private bool userIsAppliedToHomework(User user, Homework homework)
        {
            switch (user.Role)
            {
                case Domain.User.UserRole.Student:
                    if (homework.Students.Any(student => student.Id == user.Id))
                        return true;
                    break;
                case Domain.User.UserRole.Teacher:
                    if (homework.Graders.Any(grader => grader.Id == user.Id))
                        return true;
                    break;
                case Domain.User.UserRole.Unknown:
                default:
                    return false;
            }

            return false;
        }
    }
}
