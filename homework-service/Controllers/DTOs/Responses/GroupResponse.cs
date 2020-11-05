using homework_service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_service.Controllers.DTOs.Responses
{
    public class GroupResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Guid> students { get; set; }
        public IEnumerable<Guid> teachers { get; set; }
        public string Code { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerUsername { get; set; }
        public IEnumerable<HomeworkResponse> Homeworks { get; set; }

        public GroupResponse(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            students = group.Students.Select(s => s.Id);
            teachers = group.Teachers.Select(t => t.Id);
            Code = group.Code;
            OwnerId = group.Owner.Id;
            OwnerUsername = group.Owner.UserFullName;
            Homeworks = group.Homeworks.Select(h => new HomeworkResponse(h));
        }
    }
}
