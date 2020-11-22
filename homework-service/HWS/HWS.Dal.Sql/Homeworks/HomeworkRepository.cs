using HWS.Dal.Common;
using HWS.Dal.Mongo.Users;
using HWS.Dal.Sql.Assignments.DbEntities;
using HWS.Dal.Sql.Comments.DbEntities;
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
        private DbSet<Comment> _comments => context.Comments;

        private IUserRepoitory userRepoitory;

        public HomeworkRepository(HWSContext context, IUserRepoitory userRepoitory)
        {
            this.context = context;
            this.userRepoitory = userRepoitory;
        }

        public async Task<Domain.Homework> FindById(Guid id)
        {
            var query = await _homeworks.AsNoTracking()
                .Include(h => h.Students)
                    .ThenInclude(hsj => hsj.Student)
                .Include(h => h.Graders)
                    .ThenInclude(hgj => hgj.Grader)
                .Include(h => h.Assignments)
                .Include(h => h.Group)
                .Include(h => h.Comments)
                .Where(h => h.Id == id)
                .SingleOrDefaultAsync();

            var homework = query.ToDomainOrNull(HomeworkConverter.ToDomain);

            var studentIds = query.Students.Select(hsj => hsj.Student.UserId);
            var graderIds = query.Graders.Select(hgj => hgj.Grader.UserId);

            homework.Students = (await userRepoitory.FindAllById(studentIds).ConfigureAwait(false)).ToList();
            homework.Graders = (await userRepoitory.FindAllById(graderIds).ConfigureAwait(false)).ToList();
            homework.Group = query.Group.ToDomainOrNull(GroupConverter.ToDomain);
            homework.Assignments = query.Assignments.ToDomainOrNull(AssingmentConverter.ToDomain).ToList();

            return homework;
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

            var newAssigment = dbAssignment.ToDomainOrNull(AssingmentConverter.ToDomain);
            newAssigment.Student = assignment.Student;
            newAssigment.ReservedBy = new Domain.User();
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
                var newAssigment = a.ToDomainOrNull(AssingmentConverter.ToDomain);
                newAssigment.Student = assignments.First(assignment => assignment.Id == a.Id)?.Student;
                newAssigment.ReservedBy = new Domain.User();
                newAssigment.Homework = homework.ToDomainOrNull(HomeworkConverter.ToDomain);
                newAssigment.Group = homework.Group.ToDomainOrNull(GroupConverter.ToDomain);
                return newAssigment;
            }).ToList();
        }

        public async Task<Domain.Comment> InsertComment(Domain.User user, Domain.Homework homework, Domain.Comment comment)
        {
            var dbComment = CommentConverter.ToHomeworkDalNew(comment);
            dbComment.HomeworkId = homework._id;

            _comments.Add(dbComment);
            await context.SaveChangesAsync();

            var newComment = CommentConverter.ToHomeworkDomain(dbComment);

            return newComment;
        }
    }
}
