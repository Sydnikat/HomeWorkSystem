using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework_service.Controllers.DTOs.Responses;
using homework_service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace homework_service.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var users = await userService.GetUsers().ConfigureAwait(false);
            var userResponses = users.Select(u => new UserResponse(u));
            return Ok(userResponses);
        }
    }
}
