using homework_service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Dal.Repositories
{
    public interface IUserRepoitory
    {
        Task<User> FindById(Guid id);

        Task<User> FindByUserName(string userName);

        Task<IEnumerable<User>> FindAllById(IEnumerable<Guid> ids);

        Task<IEnumerable<User>> FindAll();
    }
}
