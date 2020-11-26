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

        public async Task<Domain.Assignment> FindById(Guid id)
        {
            var query = await _assignments.AsNoTracking()
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Group)
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Students)
                        .ThenInclude(hj => hj.Student)
                .Include(a => a.Homework)
                    .ThenInclude(h => h.Graders)
                        .ThenInclude(hj => hj.Grader)
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();

            var assignment = query.ToDomainOrNull(AssingmentConverter.ToDomain);

            if (assignment == null)
                return null;

            assignment.Student = await userRepoitory.FindById(query.Student).ConfigureAwait(false);
            var reservedBy = await userRepoitory.FindById(query.ReservedBy).ConfigureAwait(false);
            assignment.ReservedBy = reservedBy != null ? reservedBy : new Domain.User();

            assignment.Homework = query.Homework.ToDomainOrNull(HomeworkConverter.ToDomain);
            var studentIds = query.Homework.Students.Select(hsj => hsj.Student.UserId);
            var graderIds = query.Homework.Graders.Select(hgj => hgj.Grader.UserId);
            assignment.Homework.Students = (await userRepoitory.FindAllById(studentIds).ConfigureAwait(false)).ToList();
            assignment.Homework.Graders = (await userRepoitory.FindAllById(graderIds).ConfigureAwait(false)).ToList();

            assignment.Group = query.Homework.Group.ToDomainOrNull(GroupConverter.ToDomain);

            return assignment;
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
                var assignment = a.ToDomainOrNull(AssingmentConverter.ToDomain);

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
                var assignment = a.ToDomainOrNull(AssingmentConverter.ToDomain);

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

        public async Task<bool> UpdateGrade(Guid assignmentId, string grade)
        {
            if (grade == null)
                throw new ArgumentNullException(nameof(grade));

            var dbAssignment = await findByIdWithTracking(assignmentId);

            if (dbAssignment == null)
                return false;

            dbAssignment.Grade = grade;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateReservedBy(Guid assignmentId, Guid reservedBy)
        {
            if (reservedBy == null)
                throw new ArgumentNullException(nameof(reservedBy));

            var dbAssignment = await findByIdWithTracking(assignmentId);

            if (dbAssignment == null)
                return false;

            dbAssignment.ReservedBy = reservedBy;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateFileName(Guid assignmentId, string fileName, Guid fileId, DateTime turnInDate)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            var dbAssignment = await findByIdWithTracking(assignmentId);

            if (dbAssignment == null)
                return false;

            dbAssignment.FileName = fileName;
            dbAssignment.FileId = fileId;
            dbAssignment.TurnInDate = turnInDate;

            await context.SaveChangesAsync();

            return true;
        }

        private async Task<Assignment> findByIdWithTracking(Guid id) 
            => await _assignments
               .Where(a => a.Id == id)
               .SingleOrDefaultAsync();
    }
}
