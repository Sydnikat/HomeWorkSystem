using HWS.Dal.Sql.Homeworks;
using HWS.Domain;
using HWS.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository homeworkRepository;

        public HomeworkService(IHomeworkRepository homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        public async Task<Homework> GetHomework(Guid id)
        {
            return await homeworkRepository.FindById(id).ConfigureAwait(false);
        }

        public async Task<Assignment> CreateAssignment(User student, Group group, Homework homework)
        {
            if (group.Students.FirstOrDefault(s => s.Id == student.Id) == null)
                throw new IllegalStudentException("Student not in group");

            if (homework.Students.Any(s => s.Id == student.Id))
                throw new IllegalStudentException("Student is already on this homework");

            if (homework.CurrentNumberOfStudents == homework.MaximumNumberOfStudents)
                throw new HomeworkIsFullException("Student cannot fit in homework");

            var newAssignment = new Assignment(
                _id: 0,
                id: Guid.NewGuid(),
                group: homework.Group,
                homework: homework,
                submissionDeadline: homework.SubmissionDeadline,
                turnInDate: DateTime.MaxValue,
                fileName: "",
                fileId: Guid.Empty,
                student: student,
                reservedBy: null,
                grade: "");

            return await homeworkRepository.InsertAssignment(homework.Id, newAssignment).ConfigureAwait(false);
        }

        public async Task<Comment> CreateComment(User user, Homework homework, string content)
        {
            var comment = new Comment(
                _id: 0,
                id: Guid.NewGuid(),
                creationDate: DateTime.Now,
                createdBy: user.UserFullName,
                content: content
            );

            return await homeworkRepository.InsertComment(user, homework, comment).ConfigureAwait(false);
        }

        public bool UserIsAppliedToHomework(User user, Homework homework)
        {
            switch (user.Role)
            {
                case User.UserRole.Student:
                    if (homework.Students.Any(student => student.Id == user.Id))
                        return true;
                    break;
                case User.UserRole.Teacher:
                    if (homework.Graders.Any(grader => grader.Id == user.Id))
                        return true;
                    break;
                case User.UserRole.Unknown:
                default:
                    return false;
            }

            return false;
        }
    }
}
