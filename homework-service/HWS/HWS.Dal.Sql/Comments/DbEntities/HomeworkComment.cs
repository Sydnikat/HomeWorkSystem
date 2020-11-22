using HWS.Dal.Sql.Homeworks.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public class HomeworkComment : Comment
    {
        public HomeworkComment() : base()
        {
        }

        public HomeworkComment(long _id, Homework homework, Guid id, DateTime creationDate, string createdBy, string content)
            : base(_id, id, creationDate, createdBy, content)
        {
            HomeworkId = homework._id;
            Homework = homework;
        }
        public long HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
