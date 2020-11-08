using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Entities
{
    public abstract class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }

        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public string Content { get; set; }
    }
}
