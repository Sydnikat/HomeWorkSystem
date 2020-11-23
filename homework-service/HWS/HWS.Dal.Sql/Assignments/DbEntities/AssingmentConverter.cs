using HWS.Dal.Common;
using HWS.Dal.Common.Converters;
using HWS.Dal.Mongo;
using HWS.Dal.Mongo.Users.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Assignments.DbEntities
{
    public static class AssingmentConverter
    {
        public static Func<Assignment, Domain.Assignment> ToDomain => assignment
            => new Domain.Assignment(
                id: assignment.Id,
                group: null,
                homework: null,
                submissionDeadline: assignment.SubmissionDeadline,
                turnInDate: assignment.TurnInDate,
                fileName: assignment.FileName,
                fileId: assignment.FileId,
                student: null,
                reservedBy: null,
                grade: assignment.Grade,
                _id: assignment._id
            );

        public static Func<Domain.Assignment, Assignment> ToDalNew => assignment 
            => new Assignment(
                _id: 0,
                id: assignment.Id,
                homeworkId: 0,
                homework: null,
                submissionDeadline: assignment.SubmissionDeadline,
                turnInDate: assignment.TurnInDate,
                fileName: assignment.FileName,
                fileId: assignment.FileId,
                student: assignment.Student.Id,
                reservedBy: Guid.Empty,
                grade: assignment.Grade
            );

        public static Func<Domain.Assignment, Assignment> ToDal => assignment
            => new Assignment(
                _id: assignment._id,
                id: assignment.Id,
                homeworkId: 0,
                homework: null,
                submissionDeadline: assignment.SubmissionDeadline,
                turnInDate: assignment.TurnInDate,
                fileName: assignment.FileName,
                fileId: assignment.FileId,
                student: assignment.Student.Id,
                reservedBy: assignment.ReservedBy.Id,
                grade: assignment.Grade
            );
    }
}
