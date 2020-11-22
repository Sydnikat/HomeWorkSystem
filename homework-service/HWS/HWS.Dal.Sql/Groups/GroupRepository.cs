using AutoMapper;
using HWS.Dal.Common;
using HWS.Dal.Mongo.Users;
using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Groups
{
    public class GroupRepository : IGroupRepository
    {
        private readonly HWSContext context;

        private DbSet<Group> _groups => context.Groups;
        private DbSet<Comment> _comments => context.Comments;

        private IUserRepoitory userRepoitory;

        public GroupRepository(HWSContext context, IUserRepoitory userRepoitory)
        {
            this.context = context;
            this.userRepoitory = userRepoitory;
        }

        public async Task<Domain.Group> Insert(Domain.Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            var dbGroup = group.ToDalOrNull(GroupConverter.ToDalNew);

            await _groups.AddAsync(dbGroup);

            await context.SaveChangesAsync();

            var newGroup = dbGroup.ToDomainOrNull(GroupConverter.ToDomain);
            newGroup.Owner = group.Owner;
            newGroup.Students = group.Students;
            newGroup.Teachers = group.Teachers;

            return newGroup;
        }

        public async Task<Domain.Group> FindById(Guid id)
        {
            var query = await _groups.AsNoTracking()
                .Include(g => g.Students)
                    .ThenInclude(gsj => gsj.Student)
                .Include(g => g.Teachers)
                    .ThenInclude(gtj => gtj.Teacher)
                .Include(g => g.Homeworks)
                    .ThenInclude(h => h.Students)
                        .ThenInclude(hj => hj.Student)
                .Include(g => g.Homeworks)
                    .ThenInclude(h => h.Graders)
                        .ThenInclude(hj => hj.Grader)
                .Include(g => g.Homeworks)
                    .ThenInclude(h => h.Comments)
                .Include(g => g.Comments)
                .Where(g => g.Id == id)
                .SingleOrDefaultAsync();

            var group = query.ToDomainOrNull(GroupConverter.ToDomain);

            if (group == null)
                return null;

            var studentIds = query.Students.Select(gsj => gsj.Student.UserId);
            var teacherIds = query.Teachers.Select(gtj => gtj.Teacher.UserId);

            group.Owner = await userRepoitory.FindById(query.Owner).ConfigureAwait(false);
            group.Students = (await userRepoitory.FindAllById(studentIds).ConfigureAwait(false)).ToList();
            group.Teachers = (await userRepoitory.FindAllById(teacherIds).ConfigureAwait(false)).ToList();

            var homeworks = query.Homeworks.Select(h =>
            {
                var homework = h.ToDomainOrNull(HomeworkConverter.ToDomain);

                var studentIds = h.Students.Select(hsj => hsj.Student.UserId);
                var graderIds = h.Graders.Select(hgj => hgj.Grader.UserId);

                homework.Students = group.Students.Where(s => studentIds.Contains(s.Id)).ToList();
                homework.Graders = group.Teachers.Where(t => graderIds.Contains(t.Id)).ToList();
                homework.Group = group;

                return homework;
            }).ToList();

            group.Homeworks = homeworks;

            return group;
        }

        public async Task<Domain.Comment> InsertComment(Domain.User user, Domain.Group group, Domain.Comment comment)
        {
            var dbComment = CommentConverter.ToGroupDalNew(comment);
            dbComment.GroupId = group._id;

            _comments.Add(dbComment);
            await context.SaveChangesAsync();

            var newComment = CommentConverter.ToGroupDomain(dbComment);

            return newComment;
        }

        public async Task<Domain.Homework> InsertHomework(Guid groupId, Domain.Homework homework)
        {
            if (homework == null)
                throw new ArgumentNullException(nameof(homework));

            var dbHomework = homework.ToDalOrNull(HomeworkConverter.ToDalNew);

            var group = await _groups
                .Include(g => g.Homeworks)
                .Where(g => g.Id == groupId)
                .SingleOrDefaultAsync();

            if (group == null)
                return null;

            group.Homeworks.Add(dbHomework);

            await context.SaveChangesAsync();

            var newHomework = dbHomework.ToDomainOrNull(HomeworkConverter.ToDomain);
            newHomework.Students = homework.Students;
            newHomework.Graders = homework.Graders;
            newHomework.Group = group.ToDomainOrNull(GroupConverter.ToDomain);

            return newHomework;
        }

        public async Task<IReadOnlyCollection<Domain.Group>> FindAllByInStudents(Domain.User student)
        {
            return await findAllByQuery(g => g.Students.Select(gsj => gsj.Student.UserId).Contains(student.Id)).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Domain.Group>> FindAllByInTeachersOrIsOwner(Domain.User teacher)
        {
            return await findAllByQuery(g => g.Owner == teacher.Id || g.Teachers.Select(gtj => gtj.Teacher.UserId).Contains(teacher.Id)).ConfigureAwait(false);
        }

        private async Task<IReadOnlyCollection<Domain.Group>> findAllByQuery(System.Linq.Expressions.Expression<Func<Group, bool>> predicate)
        {
            var query = await _groups.AsNoTracking()
                .Include(g => g.Students)
                    .ThenInclude(gsj => gsj.Student)
                .Include(g => g.Teachers)
                    .ThenInclude(gtj => gtj.Teacher)
                .Include(g => g.Homeworks)
                    .ThenInclude(h => h.Students)
                        .ThenInclude(hj => hj.Student)
                .Include(g => g.Homeworks)
                    .ThenInclude(h => h.Graders)
                        .ThenInclude(hj => hj.Grader)
                .Include(g => g.Homeworks)
                    .ThenInclude(h => h.Comments)
                .Include(g => g.Comments)
                .Where(predicate)
                .ToListAsync();

            var groupTasks = query.Select(async g =>
            {
                var group = g.ToDomainOrNull(GroupConverter.ToDomain);

                var studentIds = g.Students.Select(gsj => gsj.Student.UserId);
                var teacherIds = g.Teachers.Select(gtj => gtj.Teacher.UserId);

                group.Owner = await userRepoitory.FindById(g.Owner).ConfigureAwait(false);
                group.Students = (await userRepoitory.FindAllById(studentIds).ConfigureAwait(false)).ToList();
                group.Teachers = (await userRepoitory.FindAllById(teacherIds).ConfigureAwait(false)).ToList();

                var homeworks = g.Homeworks.Select(h =>
                {
                    var homework = h.ToDomainOrNull(HomeworkConverter.ToDomain);

                    var studentIds = h.Students.Select(hsj => hsj.Student.UserId);
                    var graderIds = h.Graders.Select(hgj => hgj.Grader.UserId);

                    homework.Students = group.Students.Where(s => studentIds.Contains(s.Id)).ToList();
                    homework.Graders = group.Teachers.Where(t => graderIds.Contains(t.Id)).ToList();
                    homework.Group = group;

                    return homework;
                }).ToList();

                group.Homeworks = homeworks;

                return group;
            });


            var groups = await Task.WhenAll(groupTasks);

            return groups;
        }
    }
}
