using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Entities
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }
        public Guid Id { get; set; }
        public long HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime TurnInDate { get; set; }
        public string FileName { get; set; }
        public Guid Student { get; set; }
        public Guid ReservedBy { get; set; }
        public string Grade { get; set; }
    }
}
