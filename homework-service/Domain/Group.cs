using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Domain
{
    public class Group
    {
        public long _id { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Students { get; set; }
        public ICollection<User> Teachers { get; set; }
        public string Code { get; set; }
        public IEnumerable<Homework> Homeworks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public User Owner { get; set; }
    }
}
