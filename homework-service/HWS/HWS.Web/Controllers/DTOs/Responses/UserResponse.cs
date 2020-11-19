using HWS.Domain;
using System;
using static HWS.Domain.User;

#nullable enable

namespace HWS.Controllers.DTOs.Responses
{
    public class UserResponse
    {
        public Guid? Id { get; }
        public string UserName { get; }
        public string UserFullName { get; }
        public UserRole? Role { get; }

        public UserResponse(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            UserFullName = user.UserFullName;
            Role = user.Role;
        }

        public UserResponse(string userName, string userFullName, Guid? id = null, UserRole? role = null)
        {
            Id = id;
            UserName = userName;
            UserFullName = userFullName;
            Role = role;
        }

        public static UserResponse ToPublicUserResponse(User user)
        {
            return new UserResponse
            (
                id: null,
                userName: user.UserName,
                userFullName: user.UserFullName,
                role: null
            );
        }
    }
}
