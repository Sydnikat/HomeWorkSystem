using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public class GroupStudent : MongoUser
    {
        public GroupStudent() : base()
        {
        }

        public GroupStudent(Guid userId, long? _id = null) 
            : base(userId, _id)
        {
            
        }

        public ICollection<GroupStudentJoin> Groups { get; set; } = new List<GroupStudentJoin>();
    }
}
