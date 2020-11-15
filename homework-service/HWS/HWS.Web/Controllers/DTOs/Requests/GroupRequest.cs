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

        public GroupRequest(string name, IEnumerable<Guid> students, IEnumerable<Guid> teachers)
        {
            Name = name;
            this.students = students;
            this.teachers = teachers;
        }

        public string Name { get; set; }
        public IEnumerable<Guid> students { get; set; }
        public IEnumerable<Guid> teachers { get; set; }

        public Group ToNew()
        {
            return new Group(
                _id: 0,
                id: Guid.NewGuid(),
                name: Name,
                students: new List<User>(),
                teachers: new List<User>(),
                code: "",
                homeworks: new List<Homework>(),
                comments: new List<Comment>(),
                owner: new User(Guid.NewGuid()));
        }
    }
}
