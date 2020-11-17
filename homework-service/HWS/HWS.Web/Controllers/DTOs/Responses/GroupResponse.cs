using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWS.Controllers.DTOs.Responses
{
    public class GroupResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserResponse> students { get; set; }
        public IEnumerable<UserResponse> teachers { get; set; }
        public string Code { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerUsername { get; set; }
        public IEnumerable<HomeworkResponse> Homeworks { get; set; }

        public GroupResponse() { }

        public static GroupResponse ForTeacher(Group group)
        {
            return new GroupResponse()
            {
                Id = group.Id,
                Name = group.Name,
                students = group.Students.Select(s => new UserResponse(s)),
                teachers = group.Teachers.Select(t => new UserResponse(t)),
                Code = group.Code,
                OwnerId = group.Owner.Id,
                OwnerUsername = group.Owner.UserFullName,
                Homeworks = group.Homeworks.Select(HomeworkResponse.ForTeacher)
            };
        }

        public static GroupResponse ForStudent(Group group)
        {
            return new GroupResponse()
            {
                Id = group.Id,
                Name = group.Name,
                students = new List<UserResponse>(),
                teachers = group.Teachers.Select(t => new UserResponse(t)),
                Code = group.Code,
                OwnerId = group.Owner.Id,
                OwnerUsername = group.Owner.UserFullName,
                Homeworks = group.Homeworks.Select(HomeworkResponse.ForStudent)
            };
        }
    }
}
