using HWS.Dal.Common;
using HWS.Dal.Mongo.Users.DbEntities;
using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using HWS.Dal.Sql.MongoUsers.JoinTables;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWS.Dal.Sql.Groups.DbEntities
{
    public static class GroupConverter
    {
        public static Func<Group, Domain.Group> toDomain => group
            => new Domain.Group(
                id: group.Id,
                name: group.Name,
                students: new List<Domain.User>(),
                teachers: new List<Domain.User>(),
                code: group.Code,
                homeworks: group.Homeworks.ToDomainOrNull(HomeworkConverter.toDomain).ToList(),
                comments: group.Comments.ToDomainOrNull(CommentConverter.ToGroupDomain).ToList(),
                owner: null,
                _id: group._id
                );

        public static Func<Domain.Group, Group> toDalNew => group =>
        {
            var entity = new Group(
                _id: 0,
                id: group.Id,
                name: group.Name,
                owner: group.Owner!.Id,
                code: group.Code,
                students: new List<GroupStudentJoin>(),
                teachers: new List<GroupTeacherJoin>(),
                homeworks: new List<Homeworks.DbEntities.Homework>(),
                comments: new List<GroupComment>()
                );

            entity.Students = group.Students.Select(s => new GroupStudentJoin(s, entity)).ToList();
            entity.Teachers = group.Teachers.Select(t => new GroupTeacherJoin(t, entity)).ToList();

            return entity;
        };

        public static Func<Domain.Group, Group> toDal => group =>
        {
            var entity = new Group(
                _id: group._id,
                id: group.Id,
                name: group.Name,
                owner: group.Owner!.Id,
                code: group.Code,
                students: new List<GroupStudentJoin>(),
                teachers: new List<GroupTeacherJoin>(),
                homeworks: new List<Homeworks.DbEntities.Homework>(),
                comments: new List<GroupComment>()
                );

            entity.Students = group.Students.Select(s => new GroupStudentJoin(s, entity)).ToList();
            entity.Teachers = group.Teachers.Select(t => new GroupTeacherJoin(t, entity)).ToList();

            entity.Comments = group.Comments.Select(gc =>
            {
                var groupComment = gc.ToDalOrNull(CommentConverter.ToGroupDalNew);
                groupComment.Group = entity;
                groupComment.GroupId = entity._id;

                return groupComment;
            }).ToList();

            return entity;
        };
    }
}
