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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long _id { get; set; }
        public Guid UserId { get; set; }
    }
}
