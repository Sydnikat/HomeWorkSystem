using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Mongo.Users
{
    public interface IUserRepoitory
    {
        Task<User> FindById(Guid id);

        Task<User> FindByUserName(string userName);

        Task<IReadOnlyCollection<User>> FindAllById(IEnumerable<Guid> ids);

        Task<IReadOnlyCollection<User>> FindAll();
    }
}
