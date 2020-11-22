using HWS.Dal.Sql.Assignments;
using HWS.Domain;
using HWS.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            this.assignmentRepository = assignmentRepository;
        }

        public async Task<Assignment> GetAssignment(Guid id)
        {
            return await assignmentRepository.FindById(id).ConfigureAwait(false);
        }

        public async Task<bool> GradeAssignment(User grader, Assignment assignment, string grade)
        {
            if (!canUserManageAssignment(grader, assignment))
                throw new IllegalTeacherException("Teacher cannot grade this assignment");

            if (assignment.ReservedBy.Id == Guid.Empty)
                throw new AssignmentNotReservedException("assignment is not reserved yet");

            if (assignment.ReservedBy.Id != grader.Id)
                throw new AssignmentNotFreeException($"assignment is already reserved by ${assignment.ReservedBy}");

            return await assignmentRepository.UpdateGrade(assignment.Id, grade).ConfigureAwait(false);
        }

        public async Task<bool> ReserveAssignment(User grader, Assignment assignment)
        {
            if (!canUserManageAssignment(grader, assignment))
                throw new IllegalTeacherException("Teacher cannot reserve this assignment");

            if (assignment.ReservedBy.Id != Guid.Empty)
                throw new AssignmentNotFreeException($"assignment is already reserved by ${assignment.ReservedBy}");

            return await assignmentRepository.UpdateReservedBy(assignment.Id, grader.Id).ConfigureAwait(false);
        }

        public async Task<bool> FreeAssignment(User grader, Assignment assignment)
        {
            if (!canUserManageAssignment(grader, assignment))
                throw new IllegalTeacherException("Teacher cannot free this assignment");

            if (assignment.ReservedBy.Id == Guid.Empty)
                return true;

            if (assignment.ReservedBy.Id != grader.Id)
                throw new AssignmentNotFreeException("Teacher cannot free this assignment reserved by someone else");

            return await assignmentRepository.UpdateReservedBy(assignment.Id, Guid.Empty).ConfigureAwait(false);
        }

        public async Task<bool> ChangeAssignmentFile(Assignment assignment, string fileName)
        {
            return await assignmentRepository.UpdateFileName(assignment.Id, fileName).ConfigureAwait(false);
        }

        public async Task<ICollection<Assignment>> GetAssignmentsForStudent(User student)
        {
            return (await assignmentRepository.FindAllByStudent(student).ConfigureAwait(false)).ToList();
        }

        public async Task<ICollection<Assignment>> GetAssignmentsForTeacher(User teacher)
        {
            return (await assignmentRepository.FindAllByUserInGraders(teacher).ConfigureAwait(false)).ToList();
        }

        private bool canUserManageAssignment(User user, Assignment assignment)
            => assignment.Homework.Graders.FirstOrDefault(g => g.Id == user.Id) != null;
    }
}
