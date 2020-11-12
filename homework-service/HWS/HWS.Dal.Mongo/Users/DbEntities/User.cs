using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static HWS.Dal.Common.UserCommon;

namespace HWS.Dal.Mongo.Users.DbEntities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("id")]
        public Guid Id { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("userFullName")]
        public string UserFullName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }


        [BsonRepresentation(BsonType.String)]
        [BsonElement("role")]
        public UserRole Role { get; set; }
    }
}
