using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Repositories
{
    public interface IUserRepoitory
    {
        Task<User> FindById(Guid id);

        Task<User> FindByUserName(string userName);

        Task<IEnumerable<User>> FindAllById(IEnumerable<Guid> ids);

        Task<IEnumerable<User>> FindAll();
    }
}
