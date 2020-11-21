using HWS.Dal.Sql.Assignments;
using HWS.Domain;
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

        public async Task<ICollection<Assignment>> GetAssignmentsForStudent(User student)
        {
            return (await assignmentRepository.FindAllByStudent(student).ConfigureAwait(false)).ToList();
        }

        public async Task<ICollection<Assignment>> GetAssignmentsForTeacher(User teacher)
        {
            return (await assignmentRepository.FindAllByUserInGraders(teacher).ConfigureAwait(false)).ToList();
        }
    }
}
