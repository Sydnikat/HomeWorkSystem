using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public abstract class MongoUser
    {
        protected MongoUser()
        {
        }

        protected MongoUser(Guid userId, long? _id = null)
        {
            this._id = _id;
            this.UserId = userId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? _id { get; set; }
        public Guid UserId { get; set; }
    }
}
