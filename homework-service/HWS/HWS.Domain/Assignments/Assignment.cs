using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class Assignment
    {
        public Assignment(long _id, Guid id, Group group, Homework homework, DateTime submissionDeadline, DateTime turnInDate, string fileName, User student, User reservedBy, string grade)
        {
            this._id = _id;
            this.Id = id;
            this.Group = group;
            this.Homework = homework;
            this.SubmissionDeadline = submissionDeadline;
            this.TurnInDate = turnInDate;
            this.FileName = fileName;
            this.Student = student;
            this.ReservedBy = reservedBy;
            this.Grade = grade;
        }

        public long _id { get; set; }
        public Guid Id { get; set; }
        public Group Group { get; set; }
        public Homework Homework { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime TurnInDate { get; set; }
        public string FileName { get; set; }
        public User Student { get; set; }
        public User ReservedBy { get; set; }
        public string Grade { get; set; }
    }
}
