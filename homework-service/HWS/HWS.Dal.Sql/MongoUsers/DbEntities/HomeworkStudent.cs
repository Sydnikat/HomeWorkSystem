using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public class HomeworkStudent : MongoUser
    {
        public HomeworkStudent() : base()
        {
        }

        public HomeworkStudent(Guid userId, long? _id = null)
            : base(userId, _id)
        {

        }

        public ICollection<HomeworkStudentJoin> Homeworks { get; set; } = new List<HomeworkStudentJoin>();
    }
}
