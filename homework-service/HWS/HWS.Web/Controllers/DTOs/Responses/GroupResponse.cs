using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWS.Controllers.DTOs.Responses
{
    public class GroupResponse
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public IEnumerable<UserResponse> students { get; }
        public IEnumerable<UserResponse> teachers { get; }
        public string Code { get; }
        public Guid OwnerId { get; }
        public string OwnerFullName { get; }
        public IEnumerable<HomeworkResponse> Homeworks { get; }

        public GroupResponse() { }

        public GroupResponse(
            Guid id, 
            string name,
            IEnumerable<UserResponse> students,
            IEnumerable<UserResponse> teachers, 
            string code, 
            Guid ownerId,
            string ownerFullName,
            IEnumerable<HomeworkResponse> homeworks)
        {
            Id = id;
            Name = name;
            this.students = students;
            this.teachers = teachers;
            Code = code;
            OwnerId = ownerId;
            OwnerFullName = ownerFullName;
            Homeworks = homeworks;
        }

        public static GroupResponse ForTeacher(Group group)
        {
            return new GroupResponse(
                id: group.Id,
                name: group.Name,
                students: group.Students.Select(s => new UserResponse(s)),
                teachers: group.Teachers.Select(t => new UserResponse(t)),
                code: group.Code,
                ownerId: group.Owner.Id,
                ownerFullName: group.Owner.UserFullName,
                homeworks: group.Homeworks.Select(HomeworkResponse.ForTeacher)
                );
        }

        public static GroupResponse ForStudent(Group group)
        {
            return new GroupResponse(
                id: group.Id,
                name: group.Name,
                students: new List<UserResponse>(),
                teachers: group.Teachers.Select(t => new UserResponse(t)),
                code: group.Code,
                ownerId: group.Owner.Id,
                ownerFullName: group.Owner.UserFullName,
                homeworks: group.Homeworks.Select(HomeworkResponse.ForStudent)
                );
        }
    }
}
