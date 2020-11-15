using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class Comment
    {
        public Comment(long _id, Guid id, DateTime creationDate, string createdBy, string content)
        {
            this._id = _id;
            this.Id = id;
            this.CreationDate = creationDate;
            this.CreatedBy = createdBy;
            this.Content = content;
        }

        public long _id { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string Content { get; set; }
    }
}
