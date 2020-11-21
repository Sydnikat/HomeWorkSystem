using HWS.Dal.Sql.Groups;
using HWS.Dal.Common;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HWS.Services.Exceptions;

namespace HWS.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUserService userService;

        public GroupService(IGroupRepository groupRepository, IUserService userService)
        {
            this.groupRepository = groupRepository;
            this.userService = userService;
        }

        public async Task<Group> CreateGroup(User owner, Group newGroup, ICollection<Guid> students, ICollection<Guid> teachers)
        {
            newGroup.Owner = owner;
            newGroup.Code = generateCode();
            newGroup.Id = Guid.NewGuid();

            if (teachers.FirstOrDefault(t => t == owner.Id) == Guid.Empty)
            {
                teachers.Add(owner.Id);
            }

            IReadOnlyCollection<User> studentList = await userService.GetUsers(students.Distinct()).ConfigureAwait(false);
            IReadOnlyCollection<User> teacherList = await userService.GetUsers(teachers.Distinct()).ConfigureAwait(false);

            newGroup.Students = studentList.ToList();
            newGroup.Teachers = teacherList.ToList();

            return await groupRepository.Insert(newGroup);
        }

        public async Task<Group> GetGroup(Guid id)
        {
            return await groupRepository.FindById(id);
        }

        public async Task<Homework> CreateHomework(Group group, Homework newHomework, ICollection<Guid> students, ICollection<Guid> graders)
        {
            if (graders.FirstOrDefault(t => t == group.Owner.Id) == Guid.Empty)
            {
                graders.Add(group.Owner.Id);
            }

            IReadOnlyCollection<User> studentList = await userService.GetUsers(students.Distinct()).ConfigureAwait(false);
            IReadOnlyCollection<User> graderList = await userService.GetUsers(graders.Distinct()).ConfigureAwait(false);

            if (studentList.Any(student => !group.Students.Select(s => s.Id).Contains(student.Id)))
                throw new IllegalStudentException("Student not in group");

            if (graderList.Any(grader => !group.Teachers.Select(t => t.Id).Contains(grader.Id)))
                throw new IllegalTeacherException("Teacher not in group");

            newHomework.Students = studentList.ToList();
            newHomework.Graders = graderList.ToList();

            if (studentList.Count != 0 && newHomework.MaximumNumberOfStudents != studentList.Count)
                throw new StudentNumberMisMatchException("Number of found and expected students are not the same");

            newHomework.CurrentNumberOfStudents = newHomework.Students.Count;

            newHomework.Id = Guid.NewGuid();
            newHomework.Group = group;

            var assignments = newHomework.Students
                .Select(s => new Assignment(
                    _id: 0, 
                    id: Guid.NewGuid(),
                    group: group,
                    homework: newHomework,
                    submissionDeadline: newHomework.SubmissionDeadline,
                    turnInDate: DateTime.MaxValue,
                    fileName: "",
                    student: s,
                    reservedBy: null, 
                    grade: ""))
                .ToList();
           
            newHomework.Assignments = assignments;

            return await groupRepository.InsertHomework(group.Id, newHomework).ConfigureAwait(false);
        }

        public async Task<ICollection<Group>> GetGroupsForStudent(User student)
        {
            return (await groupRepository.FindAllByInStudents(student).ConfigureAwait(false)).ToList();
        }

        public async Task<ICollection<Group>> GetGroupsForTeacher(User teacher)
        {
            return (await groupRepository.FindAllByInTeachersOrIsOwner(teacher).ConfigureAwait(false)).ToList();
        }

        public bool CanCreateGroup(User user)
        {
            return user.Role == User.UserRole.Teacher;
        }

        private string generateCode()
        {
            const string src = "0123456789aAbBcCdDeEfFgGhHiIjJkK0123456789lLmMnNoOpPqQrRsStTuUvVwWxXyYzZ0123456789";
            int length = 8;
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
