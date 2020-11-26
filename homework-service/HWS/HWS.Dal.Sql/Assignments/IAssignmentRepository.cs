using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Assignments
{
    public interface IAssignmentRepository
    {
        Task<Assignment> FindById(Guid id);

        Task<IEnumerable<Assignment>> FindAllByStudent(User student);

        Task<IEnumerable<Assignment>> FindAllByUserInGraders(User grader);

        Task<bool> UpdateGrade(Guid assignmentId, string grade);

        Task<bool> UpdateReservedBy(Guid assignmentId, Guid reservedBy);

        Task<bool> UpdateFileName(Guid assignmentId, string fileName, Guid fileId, DateTime turnInDate);
    }
}
