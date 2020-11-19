using HWS.Dal.Sql.Homeworks.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class HomeworkStudentJoin
    {
        public HomeworkStudentJoin(HomeworkStudent student, Homework homework, long? homeworkId = null, long? studentId = null)
        {
            StudentId = studentId;
            Student = student;
            HomeworkId = homeworkId;
            Homework = homework;
        }

        public HomeworkStudentJoin(Domain.User student, Homework homework)
        {
            this.Student = new HomeworkStudent(student.Id);
            this.Homework = homework;
        }

        public HomeworkStudentJoin()
        {
        }

        public long? StudentId { get; set; }
        public HomeworkStudent Student { get; set; }
        public long? HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
