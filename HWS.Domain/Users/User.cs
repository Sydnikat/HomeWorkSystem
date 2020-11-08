using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class User
    {
        public enum UserRole
        {
            Student,
            Teacher
        }

        public string _id { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
