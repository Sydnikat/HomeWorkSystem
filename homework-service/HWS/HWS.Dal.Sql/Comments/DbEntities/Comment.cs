using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public abstract class Comment
    {
        protected Comment()
        {
        }

        protected Comment(long _id,  Guid id, DateTime creationDate, string createdBy, string content)
        {
            this._id = _id;
            this.Id = id;
            this.CreationDate = creationDate;
            this.CreatedBy = createdBy;
            this.Content = content;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }

        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public string Content { get; set; }
    }
}
