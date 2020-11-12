using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public static class CommentConverter
    {
        public static Domain.Comment toDomain(this GroupComment comment)
            => new Domain.Comment
            {
                _id = comment._id,
                Id = comment.Id,
                CreationDate = comment.CreationDate,
                CreatedBy = comment.CreatedBy,
                Content = comment.Content
            };

        public static Domain.Comment toDomain(this HomeworkComment comment)
            => new Domain.Comment
            {
                _id = comment._id,
                Id = comment.Id,
                CreationDate = comment.CreationDate,
                CreatedBy = comment.CreatedBy,
                Content = comment.Content
            };
    }
}
