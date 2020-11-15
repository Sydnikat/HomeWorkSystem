using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class Homework
    {
        public Homework(
            long _id,
            Guid id,
            string title, 
            string description,
            int maxFileSize,
            Group group, 
            DateTime submissionDeadline, 
            int maximumNumberOfStudents, 
            int currentNumberOfStudents,
            ICollection<User> graders,
            ICollection<User> students,
            ICollection<Comment> comments,
            ICollection<Assignment> assignments,
            DateTime applicationDeadline)
        {
            this._id = _id;
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.MaxFileSize = maxFileSize;
            this.Group = group;
            this.SubmissionDeadline = submissionDeadline;
            this.ApplicationDeadline = applicationDeadline;
            this.MaximumNumberOfStudents = maximumNumberOfStudents;
            this.CurrentNumberOfStudents = currentNumberOfStudents;
            this.Graders = graders;
            this.Students = students;
            this.Comments = comments;
            this.Assignments = assignments;
        }

        public long _id { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxFileSize { get; set; }
        public Group Group { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int MaximumNumberOfStudents { get; set; }
        public int CurrentNumberOfStudents { get; set; }
        public ICollection<User> Graders { get; set; }
        public ICollection<User> Students { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
