using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Groups.DbEntities
{
    public class Group
    {
        public Group()
        {
        }

        public Group(
            long _id,
            Guid id,
            string name,
            Guid owner,
            string code,
            ICollection<GroupStudentJoin> students,
            ICollection<GroupTeacherJoin> teachers,
            ICollection<Homework> homeworks,
            ICollection<GroupComment> comments)
        {
            this._id = _id;
            this.Id = id;
            this.Name = name;
            this.Students = students;
            this.Teachers = teachers;
            this.Code = code;
            this.Homeworks = homeworks;
            this.Owner = owner;
            this.Comments = comments;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupStudentJoin> Students { get; set; } = new List<GroupStudentJoin>();
        public ICollection<GroupTeacherJoin> Teachers { get; set; } = new List<GroupTeacherJoin>();
        public string Code { get; set; }
        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
        public Guid Owner { get; set; }
        public ICollection<GroupComment> Comments { get; set; } = new List<GroupComment>();
    }
}
