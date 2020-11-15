using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Domain
{
    public class Group
    {
        public Group(long _id, Guid id, string name, ICollection<User> students, ICollection<User> teachers, string code, ICollection<Homework> homeworks, ICollection<Comment> comments, User owner)
        {
            this._id = _id;
            this.Id = id;
            this.Name = name;
            this.Students = students;
            this.Teachers = teachers;
            this.Code = code;
            this.Homeworks = homeworks;
            this.Comments = comments;
            this.Owner = owner;
        }

        public long _id { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Students { get; set; } = new List<User>();
        public ICollection<User> Teachers { get; set; } = new List<User>();
        public string Code { get; set; }
        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public User Owner { get; set; }
    }
}
