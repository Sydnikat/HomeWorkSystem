using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public class HomeworkStudent : MongoUser
    {
        public ICollection<HomeworkStudentJoin> Homeworks { get; set; }
    }
}
