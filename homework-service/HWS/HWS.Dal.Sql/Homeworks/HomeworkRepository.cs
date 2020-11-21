using HWS.Dal.Common;
using HWS.Dal.Mongo.Users;
using HWS.Dal.Sql.Assignments.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Homeworks
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly HWSContext context;

        private DbSet<Homework> _homeworks => context.Homeworks;

        private IUserRepoitory userRepoitory;

        public HomeworkRepository(HWSContext context, IUserRepoitory userRepoitory)
        {
            this.context = context;
            this.userRepoitory = userRepoitory;
        }

        public async Task<Domain.Assignment> InsertAssignment(Guid homeworkId, Domain.Assignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            var dbAssignment = assignment.ToDalOrNull(AssingmentConverter.ToDalNew);

            var homework = await _homeworks
                .Include(h => h.Assignments)
                .Include(h => h.Group)
                .Where(h => h.Id == homeworkId)
                .SingleOrDefaultAsync();

            if (homework == null)
                return null;

            homework.Assignments.Add(dbAssignment);

            await context.SaveChangesAsync();

            var newAssigment = dbAssignment.ToDomainOrNull(AssingmentConverter.toDomain);
            newAssigment.Student = assignment.Student;
            newAssigment.ReservedBy = null;
            newAssigment.Homework = homework.ToDomainOrNull(HomeworkConverter.ToDomain);
            newAssigment.Group = homework.Group.ToDomainOrNull(GroupConverter.ToDomain);

            return newAssigment;
        }

        public async Task<ICollection<Domain.Assignment>> InsertAllAssignment(Guid homeworkId, ICollection<Domain.Assignment> assignments)
        {
            if (assignments == null)
                throw new ArgumentNullException(nameof(assignments));

            var dbAssignments = assignments.Select(a => a.ToDalOrNull(AssingmentConverter.ToDalNew)).ToList();

            var homework = await _homeworks
               .Include(h => h.Assignments)
               .Include(h => h.Group)
               .Where(h => h.Id == homeworkId)
               .SingleOrDefaultAsync();

            if (homework == null)
                return null;

            dbAssignments.ForEach(a => homework.Assignments.Add(a));

            await context.SaveChangesAsync();

            return dbAssignments.Select(a => {
                var newAssigment = a.ToDomainOrNull(AssingmentConverter.toDomain);
                newAssigment.Student = assignments.First(assignment => assignment.Id == a.Id)?.Student;
                newAssigment.ReservedBy = null;
                newAssigment.Homework = homework.ToDomainOrNull(HomeworkConverter.ToDomain);
                newAssigment.Group = homework.Group.ToDomainOrNull(GroupConverter.ToDomain);
                return newAssigment;
            }).ToList();
        }
    }
}
