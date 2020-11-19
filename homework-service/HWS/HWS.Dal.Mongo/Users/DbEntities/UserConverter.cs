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
        public static Func<User, Domain.User> toDomain => 
            user => new Domain.User(
                id: user.Id,
                userName: user.UserName,
                userFullName: user.UserFullName,
                password: user.Password,
                role: user.Role.toDomain());
    }
}
