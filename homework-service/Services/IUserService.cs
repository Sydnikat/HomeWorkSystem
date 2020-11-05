using homework_service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Services
{
    public interface IUserService
    {
        Task<User> GetUser(Guid id);

        Task<IEnumerable<User>> GetUsers(IEnumerable<Guid> ids);

        Task<IEnumerable<User>> GetUsers();
    }
}
