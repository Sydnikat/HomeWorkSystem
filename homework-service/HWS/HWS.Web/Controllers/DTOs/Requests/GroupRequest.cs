using HWS.Domain;
using System;
using System.Collections.Generic;

namespace HWS.Controllers.DTOs.Requests
{
    public class GroupRequest
    {
        public GroupRequest()
        {
        }

        public GroupRequest(string name, ICollection<Guid> students, ICollection<Guid> teachers)
        {
            Name = name;
            this.students = students;
            this.teachers = teachers;
        }

        public string Name { get; set; }
        public ICollection<Guid> students { get; set; }
        public ICollection<Guid> teachers { get; set; }

        public Group ToNew()
        {
            return new Group(
                _id: 0,
                id: Guid.Empty,
                name: Name,
                students: new List<User>(),
                teachers: new List<User>(),
                code: "",
                homeworks: new List<Homework>(),
                comments: new List<Comment>(),
                owner: new User());
        }
    }
}
