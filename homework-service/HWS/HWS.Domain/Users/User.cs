using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class User
    {
        public User(Guid id, string userName, string userFullName, string password, UserRole role)
        {
            Id = id;
            UserName = userName;
            UserFullName = userFullName;
            Password = password;
            Role = role;
        }

        public User(Guid id)
        {
            Id = id;
        }

        public enum UserRole
        {
            Student,
            Teacher,
            Unknown
        }

        public Guid Id { get; set; }
        public string UserName { get; set; } = "";
        public string UserFullName { get; set; } = "";
        public string Password { get; set; } = "";
        public UserRole Role { get; set; } = UserRole.Unknown;
    }
}
