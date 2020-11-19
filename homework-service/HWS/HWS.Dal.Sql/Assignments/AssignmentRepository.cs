using HWS.Dal.Common;
using HWS.Dal.Mongo.Users;
using HWS.Dal.Sql.Assignments.DbEntities;
using HWS.Domain;
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

        private DbSet<DbEntities.Assignment> _assignments => context.Assignments;

        private IUserRepoitory userRepoitory;

        public AssignmentRepository(HWSContext context, IUserRepoitory userRepoitory)
        {
            this.context = context;
            this.userRepoitory = userRepoitory;
        }
        public async Task<IEnumerable<Domain.Assignment>> FindAllByStudent(User student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var query = await _assignments.AsNoTracking()
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Group)
                .Include(a => a.Student)
                .Include(a => a.ReservedBy)
                .Where(a => a.Student == student.Id)
                .ToListAsync();

            var assignmentTasks = query.Select(async a => 
            {
                var assignment = a.ToDomainOrNull(AssingmentConverter.toDomain);
                assignment.Student = await userRepoitory.FindById(a.Student).ConfigureAwait(false);
                assignment.ReservedBy = await userRepoitory.FindById(a.ReservedBy).ConfigureAwait(false);

                return assignment;
            });

            var assignments = Task.WhenAll(assignmentTasks);

            return await assignments;
        }

        public async Task<IEnumerable<Domain.Assignment>> FindAllByUserInGraders(User grader)
        {
            if (grader == null)
                throw new ArgumentNullException(nameof(grader));

            var query = await _assignments.AsNoTracking()
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Group)
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Graders)
                        .ThenInclude(g => g.Grader)
                .Include(a => a.Student)
                .Include(a => a.ReservedBy)
                .Where(a => a.Homework.Graders.FirstOrDefault(gj => gj.Grader.UserId == grader.Id) != null)
                .ToListAsync();

            var assignments = query.Select(async a =>
            {
                var assignment = a.ToDomainOrNull(AssingmentConverter.toDomain);
                assignment.Student = await userRepoitory.FindById(a.Student).ConfigureAwait(false);
                assignment.ReservedBy = await userRepoitory.FindById(a.ReservedBy).ConfigureAwait(false);

                return assignment;
            });

            var task = Task.WhenAll(assignments);

            return await task;
        }

        public async Task<Domain.Assignment> Insert(Domain.Assignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            var dbAssignment = assignment.ToDalOrNull(AssingmentConverter.ToDalNew);

            await _assignments.AddAsync(dbAssignment);

            await context.SaveChangesAsync();

            var newAssignment = dbAssignment.ToDomainOrNull(AssingmentConverter.toDomain);
            newAssignment.Student = assignment.Student;

            return newAssignment;
        }

        public async Task<IEnumerable<Domain.Assignment>> InsertAll(ICollection<Domain.Assignment> assignments)
        {
            if (assignments == null)
                throw new ArgumentNullException(nameof(assignments));

            var dbAssignments = assignments.Select(a => a.ToDalOrNull(AssingmentConverter.ToDalNew)).ToList();

            await _assignments.AddRangeAsync(dbAssignments);

            await context.SaveChangesAsync();

            return dbAssignments.Select(a => {
                var assignment = a.ToDomainOrNull(AssingmentConverter.toDomain);
                assignment.Student = assignments.First(assignment => assignment.Id == a.Id).Student;
                return assignment;
             });
        }
    }
}
