using HWS.Dal.Common;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public static class CommentConverter
    {
        public static Func<GroupComment, Domain.Comment> ToGroupDomain => comment
            => new Domain.Comment(
                id: comment.Id,
                creationDate: comment.CreationDate,
                createdBy: comment.CreatedBy,
                content: comment.Content,
                _id: comment._id
                );

        public static Func<HomeworkComment, Domain.Comment> ToHomeworkDomain => comment
            => new Domain.Comment(
                 id: comment.Id,
                creationDate: comment.CreationDate,
                createdBy: comment.CreatedBy,
                content: comment.Content,
                _id: comment._id
                );

        public static Func<Domain.Comment, GroupComment> ToGroupDalNew => comment
            => new GroupComment(
                _id: 0,
                groupId: 0,
                group: null,
                id: Guid.Empty,
                creationDate: comment.CreationDate,
                createdBy: comment.CreatedBy,
                content: comment.Content
                );

        public static Func<Domain.Comment, HomeworkComment> ToHomeworkDalNew => comment
            => new HomeworkComment(
                _id: 0,
                homeworkId: 0,
                homework: null,
                id: Guid.Empty,
                creationDate: comment.CreationDate,
                createdBy: comment.CreatedBy,
                content: comment.Content
                );
    }
}
