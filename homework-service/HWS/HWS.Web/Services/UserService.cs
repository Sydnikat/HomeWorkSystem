using HWS.Dal.Mongo;
using HWS.Dal.Mongo.Users;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepoitory userRepoitory;

        public UserService(IUserRepoitory userRepoitory)
        {
            this.userRepoitory = userRepoitory;
        }

        public async Task<Domain.User> GetUser(Guid id)
        {
            return await userRepoitory.FindById(id);
        }

        public async Task<Domain.User> GetUser(string userName)
        {
            return await userRepoitory.FindByUserName(userName);
        }

        public async Task<IEnumerable<Domain.User>> GetUsers(IEnumerable<Guid> ids)
        {
            return await userRepoitory.FindAllById(ids);
        }

        public async Task<IEnumerable<Domain.User>> GetUsers()
        {
            return await userRepoitory.FindAll();
        }
    }
}
