using HWS.Dal.Common;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Converters
{
    public static class UserConverter
    {
        public static Domain.User toDomain(this Entities.User value)
            => new Domain.User
            {
                _id = value._id,
                Id = value.Id,
                UserName = value.UserName,
                UserFullName = value.UserFullName,
                Password = value.Password,
                Role = value.Role.toDomain()
            };
    }
}
