using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class Assignment
    {
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
