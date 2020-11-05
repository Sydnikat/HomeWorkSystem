using homework_service.Domain;
using System;
using static homework_service.Domain.User;

namespace homework_service.Controllers.DTOs.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public UserResponse(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            UserFullName = user.UserFullName;
            Password = user.Password;
            Role = user.Role;
        }
    }
}
