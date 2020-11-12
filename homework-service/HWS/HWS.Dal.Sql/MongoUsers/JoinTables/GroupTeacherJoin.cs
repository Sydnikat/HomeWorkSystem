using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class GroupTeacherJoin
    {
        public long TeacherId { get; set; }
        public GroupTeacher Teacher { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
    }
}
