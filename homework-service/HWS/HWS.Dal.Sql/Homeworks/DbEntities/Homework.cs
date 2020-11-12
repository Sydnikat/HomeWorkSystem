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
        public ICollection<HomeworkGranderJoin> Graders { get; set; }
        public ICollection<HomeworkStudentJoin> Students { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<HomeworkComment> Comments { get; set; }
    }
}
