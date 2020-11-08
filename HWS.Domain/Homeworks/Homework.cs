using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class Homework
    {
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
    }
}
