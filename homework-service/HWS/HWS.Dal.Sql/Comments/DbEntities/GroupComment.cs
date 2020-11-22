using HWS.Dal.Sql.Groups.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Comments.DbEntities
{
    public class GroupComment : Comment
    {
        public GroupComment() : base()
        {
        }

        public GroupComment(long _id, long groupId, Group group, Guid id, DateTime creationDate, string createdBy, string content) 
            : base(_id, id, creationDate, createdBy, content)
        {
            GroupId = groupId;
            Group = group;
        }

        public long GroupId { get; set; }
        public Group Group { get; set; }
    }
}
