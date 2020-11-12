using HWS.Dal.Sql.Homeworks.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public class HomeworkComment : Comment
    {
        public long HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
