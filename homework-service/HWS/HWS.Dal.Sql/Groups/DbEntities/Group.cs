using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Entities
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupStudentJoin> Students { get; set; }
        public ICollection<GroupTeacherJoin> Teachers { get; set; }
        public string Code { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public MongoUser Owner { get; set; }
        public ICollection<GroupComment> Comments { get; set; }
    }
}
