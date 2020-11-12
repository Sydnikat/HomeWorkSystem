using HWS.Dal.Common;
using HWS.Dal.Common.Converters;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Mongo.Users.DbEntities
{
    public static class UserConverter
    {
        public static Domain.User toDomain(this User value)
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
