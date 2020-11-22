using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IGroupService
    {
        bool CanCreateGroup(User user);

        bool UserIsMemberOfGroup(User user, Group group);

        Task<Group> GetGroup(Guid id);

        Task<ICollection<Group>> GetGroupsForStudent(User student);

        Task<ICollection<Group>> GetGroupsForTeacher(User teacher);

        Task<Group> CreateGroup(User owner, Group newGroup, ICollection<Guid> students, ICollection<Guid> teachers);
       
        Task<Comment> CreateGroupComment(User user, Group group, string content);
        
        Task<Homework> CreateHomework(Group group, Homework newHomework, ICollection<Guid> students, ICollection<Guid> graders);
    }
}