using HWS.Dal.Common;
using HWS.Dal.Mongo.Users;
using HWS.Dal.Sql.Groups.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Groups
{
    public class GroupRepository : IGroupRepository
    {
        private readonly HWSContext context;

        private DbSet<Group> _groups => context.Groups;

        private IUserRepoitory userRepoitory;

        public GroupRepository(HWSContext context, IUserRepoitory userRepoitory)
        {
            this.context = context;
            this.userRepoitory = userRepoitory;
        }

        public async Task<IEnumerable<Domain.Group>> FindAllByStudent(Domain.User student)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Group> Insert(Domain.Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            var dbGroup = group.ToDalOrNull(GroupConverter.toDalNew);

            await _groups.AddAsync(dbGroup);

            await context.SaveChangesAsync();

            var newGroup = dbGroup.ToDomainOrNull(GroupConverter.toDomain);
            newGroup.Owner = group.Owner;
            newGroup.Students = group.Students;
            newGroup.Teachers = group.Teachers;

            return newGroup;
        }
    }
}
