using HWS.Dal.Entities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class HomeworkGranderJoin
    {
        public long GraderId { get; set; }
        public HomeworkGrader Grader { get; set; }
        public long HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
