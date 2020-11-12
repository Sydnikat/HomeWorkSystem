using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using HWS.Dal.Mongo.Config;
using HWS.Dal.Mongo.Users.DbEntities;

namespace HWS.Dal.Mongo.Users
{
    public class UserRepository : IUserRepoitory
    {
        private readonly IMongoCollection<DbEntities.User> _users;
        public UserRepository(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<DbEntities.User>(settings.UsersCollectionName);
        }

        public async Task<IEnumerable<Domain.User>> FindAll()
        {
            var query = await _users.FindAsync(Builders<DbEntities.User>.Filter.Empty);
            var result = query.ToList();
            return result.Select(user => user.toDomain());
        }

        public async Task<IEnumerable<Domain.User>> FindAllById(IEnumerable<Guid> ids)
        {
            var query = await _users.FindAsync(user => ids.Contains(user.Id));
            var result = query.ToList();
            return result.Select(user => user.toDomain());
        }

        public async Task<Domain.User> FindById(Guid id)
        {
            var query = await _users.FindAsync(user => user.Id == id);
            var result = query.FirstOrDefault();
            if (result == null)
                return null;
            else
                return result.toDomain();
        }

        public async Task<Domain.User> FindByUserName(string userName)
        {
            var query = await _users.FindAsync(user => user.UserName == userName);
            var result = query.FirstOrDefault();
            if (result == null)
                return null;
            else
                return result.toDomain();
        }
    }
}
