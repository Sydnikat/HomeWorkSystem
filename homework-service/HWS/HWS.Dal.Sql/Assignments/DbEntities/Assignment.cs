using HWS.Dal.Sql.Homeworks.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Assignments.DbEntities
{
    public class Assignment
    {
        public Assignment()
        {
        }

        public Assignment(long _id, Guid id, long homeworkId, Homework homework, DateTime submissionDeadline, DateTime turnInDate, string fileName, Guid fileId, Guid student, Guid reservedBy, string grade)
        {
            this._id = _id;
            this.Id = id;
            this.HomeworkId = homeworkId;
            this.Homework = homework;
            this.SubmissionDeadline = submissionDeadline;
            this.TurnInDate = turnInDate;
            this.FileName = fileName;
            this.FileId = fileId;
            this.Student = student;
            this.ReservedBy = reservedBy;
            this.Grade = grade;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }
        public Guid Id { get; set; }
        public long HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime TurnInDate { get; set; }
        public string FileName { get; set; }
        public Guid FileId { get; set; }
        public Guid Student { get; set; }
        public Guid ReservedBy { get; set; }
        public string Grade { get; set; }
    }
}
