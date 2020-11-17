﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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


        // TODO: remove id and use JWT
        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> GetGroups(Guid id)
        {
            var principal = this.User;
            //var userId = Guid.Parse("6994553d-3f8f-4e90-8243-ed6980832ae9");
            var user = await userService.GetUser(id);

            if (user == null)
                return Ok(new List<Group>().Select(GroupResponse.ForTeacher));

            switch (user.Role)
            {
                case Domain.User.UserRole.Student:
                    var groupsForStudent = await groupService.GetGroupsForStudent(user).ConfigureAwait(false);

                    return Ok(groupsForStudent.Select(GroupResponse.ForStudent));

                case Domain.User.UserRole.Teacher:
                    var groupsForTeacher = await groupService.GetGroupsForTeacher(user).ConfigureAwait(false);

                    return Ok(groupsForTeacher.Select(GroupResponse.ForTeacher));

                case Domain.User.UserRole.Unknown:
                default:
                    return Ok(new List<Group>().Select(GroupResponse.ForStudent));
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> CreateGroup([FromBody] GroupRequest request)
        {
            // TODO: JWT
            var id = Guid.Parse("5ebbec4b-a5da-484e-84e5-644c3118898a");

            if (request == null)
                throw new HWSException("Request body cannot be null", StatusCodes.Status400BadRequest);

            var user = await userService.GetUser(id);

            if (!groupService.CanCreateGroup(user))
                throw new HWSException("User has to be a teacher to create a group", StatusCodes.Status403Forbidden);

            var savedGroup = await groupService
                .CreateGroup(user, request.ToNew(), request.students, request.teachers)
                .ConfigureAwait(false);

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

        [HttpPost("{id}/homeworks")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateHomework(Guid id, [FromBody] HomeworkRequest request)
        {
            // TODO: JWT
            var userId = Guid.Parse("5ebbec4b-a5da-484e-84e5-644c3118898a");

            if (request == null)
                throw new HWSException("Request body cannot be null", StatusCodes.Status400BadRequest);

            var user = await userService.GetUser(userId);

            var group = await groupService.GetGroup(id).ConfigureAwait(false);

            if (group == null)
                throw new HWSException("Group not found", StatusCodes.Status404NotFound);

            if (group.Owner.Id != user.Id)
                throw new HWSException("User has to be the group owner to create a new homework", StatusCodes.Status403Forbidden);

            try
            {
                var savedHomework = await groupService
                .CreateHomework(group, request.ToNew(), request.Students, request.Graders)
                .ConfigureAwait(false);

                return Created(nameof(CreateHomework), HomeworkResponse.ForTeacher(savedHomework));
            }
            catch (IllegalStudentException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            catch (IllegalTeacherException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            catch (StudentNumberMisMatchException e)
            {
                throw new HWSException(e.Message, StatusCodes.Status400BadRequest);
            }
            
        }
    }
}
