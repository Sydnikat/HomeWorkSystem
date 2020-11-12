using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class GroupStudentJoin
    {
        public long StudentId { get; set; }
        public GroupStudent Student { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
    }
}
