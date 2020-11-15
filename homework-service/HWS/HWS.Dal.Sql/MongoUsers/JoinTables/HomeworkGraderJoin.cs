using HWS.Dal.Sql.Homeworks.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    public class HomeworkGraderJoin
    {
        public HomeworkGraderJoin(HomeworkGrader grader, Homework homework, long? homeworkId = null, long? graderId = null)
        {
            GraderId = graderId;
            Grader = grader;
            HomeworkId = homeworkId;
            Homework = homework;
        }

        public HomeworkGraderJoin(Domain.User grader, Homework homework)
        {
            this.Grader = new HomeworkGrader(grader.Id);
            this.Homework = homework;
        }

        public HomeworkGraderJoin()
        {
        }

        public long? GraderId { get; set; }
        public HomeworkGrader Grader { get; set; }
        public long? HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
