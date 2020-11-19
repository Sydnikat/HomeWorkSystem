using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using HWS.Dal.Mongo.Config;
using HWS.Dal.Mongo.Users.DbEntities;
using MongoDB.Bson;
using HWS.Dal.Common;

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

        public async Task<IReadOnlyCollection<Domain.User>> FindAll()
        {
            var query = await _users.FindAsync(Builders<DbEntities.User>.Filter.Empty);
            return query.ToList().ToDomainOrNull(UserConverter.toDomain);
        }

        public async Task<IReadOnlyCollection<Domain.User>> FindAllById(IEnumerable<Guid> ids)
        {
            if (ids == null)
                return new List<Domain.User>();

            var filter = Builders<DbEntities.User>.Filter.In("id", ids.Select(id => id.ToString()));
            var query = await _users.FindAsync(filter);
            return query.ToList().ToDomainOrNull(UserConverter.toDomain);
        }

        public async Task<Domain.User> FindById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var filter = Builders<DbEntities.User>.Filter.Eq("id", id.ToString());
            var query = await _users.FindAsync(filter);
            return query.FirstOrDefault().ToDomainOrNull(UserConverter.toDomain);
        }

        public async Task<Domain.User> FindByUserName(string userName)
        {
            if (userName == null || userName == "")
                return null;

            var query = await _users.FindAsync(user => user.UserName == userName);
            return query.FirstOrDefault().ToDomainOrNull(UserConverter.toDomain);
        }
    }
}
