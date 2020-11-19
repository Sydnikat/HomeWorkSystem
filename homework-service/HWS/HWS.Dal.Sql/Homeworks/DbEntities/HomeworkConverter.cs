using HWS.Dal.Common;
using HWS.Dal.Common.Converters;
using HWS.Dal.Mongo.Users.DbEntities;
using HWS.Dal.Sql.Assignments.DbEntities;
using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HWS.Dal.Sql.Homeworks.DbEntities
{
    public static class HomeworkConverter
    {
        public static Func<Homework, Domain.Homework> ToDomain => homework
            => new Domain.Homework(
                id: homework.Id,
                title: homework.Title,
                description: homework.Description,
                maxFileSize: homework.MaxFileSize,
                group: null,
                submissionDeadline: homework.SubmissionDeadline,
                applicationDeadline: homework.ApplicationDeadline,
                maximumNumberOfStudents: homework.MaximumNumberOfStudents,
                currentNumberOfStudents: homework.CurrentNumberOfStudents,
                graders: new List<Domain.User>(),
                students: new List<Domain.User>(),
                comments: homework.Comments.ToDomainOrNull(CommentConverter.ToHomeworkDomain).ToList(),
                assignments: new List<Domain.Assignment>(),
                _id: homework._id
                );

        public static Func<Domain.Homework, Homework> ToDalNew => homework =>
        {
            var entity = new Homework(
                _id: 0,
                id: homework.Id,
                title: homework.Title,
                description: homework.Description,
                maxFileSize: homework.MaxFileSize,
                groupId: 0,
                group: null,
                submissionDeadline: homework.SubmissionDeadline,
                applicationDeadline: homework.ApplicationDeadline,
                maximumNumberOfStudents: homework.MaximumNumberOfStudents,
                currentNumberOfStudents: homework.CurrentNumberOfStudents,
                graders: new List<HomeworkGraderJoin>(),
                students: new List<HomeworkStudentJoin>(),
                comments: new List<HomeworkComment>(),
                assignments: new List<Assignment>()
                );

            entity.Graders = homework.Graders.Select(g => new HomeworkGraderJoin(g, entity)).ToList();
            entity.Students = homework.Students.Select(s => new HomeworkStudentJoin(s, entity)).ToList();
            entity.Assignments = homework.Assignments.Select(a => a.ToDalOrNull(AssingmentConverter.ToDalNew)).ToList();

            return entity;
        };

        public static Func<Domain.Homework, Homework> ToDal => homework =>
        {
            var entity = new Homework(
                _id: homework._id,
                id: homework.Id,
                title: homework.Title,
                description: homework.Description,
                maxFileSize: homework.MaxFileSize,
                groupId: 0,
                group: null,
                submissionDeadline: homework.SubmissionDeadline,
                applicationDeadline: homework.ApplicationDeadline,
                maximumNumberOfStudents: homework.MaximumNumberOfStudents,
                currentNumberOfStudents: homework.CurrentNumberOfStudents,
                graders: new List<HomeworkGraderJoin>(),
                students: new List<HomeworkStudentJoin>(),
                comments: new List<HomeworkComment>(),
                assignments: new List<Assignment>()
                );

            entity.Graders = homework.Graders.Select(g => new HomeworkGraderJoin(g, entity)).ToList();
            entity.Students = homework.Students.Select(s => new HomeworkStudentJoin(s, entity)).ToList();

            entity.Comments = homework.Comments.Select(gc =>
            {
                var homeworkComment = gc.ToDalOrNull(CommentConverter.ToHomeworkDalNew);
                homeworkComment.Homework = entity;
                homeworkComment.HomeworkId = entity._id;

                return homeworkComment;
            }).ToList();

            entity.Assignments = homework.Assignments.ToDalOrNull(AssingmentConverter.ToDal).ToList();

            return entity;
        };
    }
}
