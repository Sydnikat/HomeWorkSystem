using HWS.Dal.Sql.Homeworks.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class HomeworkStudentJoin
    {
        public long StudentId { get; set; }
        public HomeworkStudent Student { get; set; }
        public long HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
