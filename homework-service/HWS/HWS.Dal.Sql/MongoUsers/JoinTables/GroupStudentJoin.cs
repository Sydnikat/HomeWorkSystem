using HWS.Dal.Mongo.Users.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class GroupStudentJoin
    {
        public GroupStudentJoin(GroupStudent student, Group group, long? groupId = null, long? studentId = null)
        {
            StudentId = studentId;
            Student = student;
            GroupId = groupId;
            Group = group;
        }

        public GroupStudentJoin(Domain.User student, Group group)
        {
            this.Student = new GroupStudent(student.Id);
            this.Group = group;
        }

        public GroupStudentJoin()
        {
        }

        public long? StudentId { get; set; } = null;
        public GroupStudent Student { get; set; }
        public long? GroupId { get; set; } = null;
        public Group Group { get; set; }
    }
}
