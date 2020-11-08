using HWS.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public class GroupComment : Comment
    {
        public long GroupId { get; set; }
        public Group Group { get; set; }
    }
}
