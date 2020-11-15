using HWS.Dal.Sql.Assignments.DbEntities;
using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Homeworks.DbEntities
{
    public class Homework
    {
        public Homework()
        {
        }

        public Homework(
            long _id,
            Guid id,
            string title,
            string description,
            int maxFileSize,
            Group group, 
            DateTime submissionDeadline,
            DateTime applicationDeadline,
            int maximumNumberOfStudents, 
            int currentNumberOfStudents,
            ICollection<HomeworkGraderJoin> graders,
            ICollection<HomeworkStudentJoin> students,
            ICollection<Assignment> assignments,
            ICollection<HomeworkComment> comments)
        {
            this._id = _id;
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.MaxFileSize = maxFileSize;
            this.GroupId = group._id;
            this.Group = group;
            this.SubmissionDeadline = submissionDeadline;
            this.ApplicationDeadline = applicationDeadline;
            this.MaximumNumberOfStudents = maximumNumberOfStudents;
            this.CurrentNumberOfStudents = currentNumberOfStudents;
            this.Graders = graders;
            this.Students = students;
            this.Assignments = assignments;
            this.Comments = comments;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxFileSize { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int MaximumNumberOfStudents { get; set; }
        public int CurrentNumberOfStudents { get; set; }
        public ICollection<HomeworkGraderJoin> Graders { get; set; }
        public ICollection<HomeworkStudentJoin> Students { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<HomeworkComment> Comments { get; set; }
    }
}
