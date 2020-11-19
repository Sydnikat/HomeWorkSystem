using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public class HomeworkGrader : MongoUser
    {
        public HomeworkGrader() : base()
        {
        }

        public HomeworkGrader(Guid userId, long? _id = null)
            : base(userId, _id)
        {

        }

        public ICollection<HomeworkGraderJoin> Homeworks { get; set; } = new List<HomeworkGraderJoin>();
    }
}
