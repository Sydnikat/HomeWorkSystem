using homework_service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Dal.Converters
{
    public static class UserConverter
    {
        public static User toDomain(this Entities.User value)
            => new User
            {
                _id = value._id,
                Id = value.Id,
                UserName = value.UserName,
                UserFullName = value.UserFullName,
                Password = value.Password,
                Role = value.Role
            };
    }
}
