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
using ArgumentNullException = System.ArgumentNullException;

namespace HWS.Dal.Sql.Assignments
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly HWSContext context;

        private DbSet<Assignment> _assignments => context.Assignments;

        private IUserRepoitory userRepoitory;

        public AssignmentRepository(HWSContext context, IUserRepoitory userRepoitory)
        {
            this.context = context;
            this.userRepoitory = userRepoitory;
        }
        public async Task<IEnumerable<Domain.Assignment>> FindAllByStudent(Domain.User student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var query = await _assignments.AsNoTracking()
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Group)
                .Where(a => a.Student == student.Id)
                .ToListAsync();

            var assignmentTasks = query.Select(async a => 
            {
                var assignment = a.ToDomainOrNull(AssingmentConverter.toDomain);

                assignment.Homework = a.Homework.ToDomainOrNull(HomeworkConverter.ToDomain);
                assignment.Group = a.Homework.Group.ToDomainOrNull(GroupConverter.ToDomain);
                assignment.Student = await userRepoitory.FindById(a.Student).ConfigureAwait(false);
                var reservedBy = await userRepoitory.FindById(a.ReservedBy).ConfigureAwait(false);

                assignment.ReservedBy = reservedBy != null ? reservedBy : new Domain.User();

                return assignment;
            });

            var assignments = await Task.WhenAll(assignmentTasks);

            return assignments;
        }

        public async Task<IEnumerable<Domain.Assignment>> FindAllByUserInGraders(Domain.User grader)
        {
            if (grader == null)
                throw new ArgumentNullException(nameof(grader));

            var query = await _assignments.AsNoTracking()
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Group)
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Graders)
                        .ThenInclude(g => g.Grader)
                .Where(a => a.Homework.Graders.FirstOrDefault(gj => gj.Grader.UserId == grader.Id) != null)
                .ToListAsync();

            var assignmentTasks = query.Select(async a =>
            {
                var assignment = a.ToDomainOrNull(AssingmentConverter.toDomain);

                assignment.Homework = a.Homework.ToDomainOrNull(HomeworkConverter.ToDomain);
                assignment.Group = a.Homework.Group.ToDomainOrNull(GroupConverter.ToDomain);
                assignment.Student = await userRepoitory.FindById(a.Student).ConfigureAwait(false);
                var reservedBy = await userRepoitory.FindById(a.ReservedBy).ConfigureAwait(false);

                assignment.ReservedBy = reservedBy != null ? reservedBy : new Domain.User();

                return assignment;
            });

            var assignments = await Task.WhenAll(assignmentTasks);

            return assignments;
        } 
    }
}
