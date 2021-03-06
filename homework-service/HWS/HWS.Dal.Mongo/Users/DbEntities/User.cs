﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static HWS.Dal.Common.UserCommon;

namespace HWS.Dal.Mongo.Users.DbEntities
{
    public class User
    {
        public User(string _id, Guid id, string userName, string userFullName, string password, UserRole role)
        {
            this._id = _id;
            this.Id = id;
            this.UserName = userName;
            this.UserFullName = userFullName;
            this.Password = password;
            this.Role = role;
        }

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

        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("__v")]
        public int version { get; set; }
    }
}
