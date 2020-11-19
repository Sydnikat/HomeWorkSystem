using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public class GroupTeacher : MongoUser
    {
        public GroupTeacher() : base()
        {
        }

        public GroupTeacher(Guid userId, long? _id = null)
            : base(userId, _id)
        {
           
        }

        public ICollection<GroupTeacherJoin> Groups { get; set; } = new List<GroupTeacherJoin>();
    }
}
