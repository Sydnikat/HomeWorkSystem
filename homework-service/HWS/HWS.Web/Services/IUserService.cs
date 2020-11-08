using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IUserService
    {
        Task<User> GetUser(Guid id);

        Task<User> GetUser(string userName);

        Task<IEnumerable<User>> GetUsers(IEnumerable<Guid> ids);

        Task<IEnumerable<User>> GetUsers();
    }
}
