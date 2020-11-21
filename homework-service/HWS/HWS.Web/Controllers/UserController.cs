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
using static HWS.Middlewares.ErrorHandlerMiddleware;

namespace HWS.Controllers
{
    // ToDo: JWT
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        private User getUser() => (User)HttpContext?.Items["User"];

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var teacher = getUser();

            if (teacher.Role != Domain.User.UserRole.Teacher)
                throw new HWSException("User has to be a teacher", StatusCodes.Status403Forbidden);

            var users = await userService.GetUsers().ConfigureAwait(false);

            if (users == null)
                users = new List<User>();

            var userResponses = users.Select(u => new UserResponse(u));
            return Ok(userResponses);
        }
    }
}
