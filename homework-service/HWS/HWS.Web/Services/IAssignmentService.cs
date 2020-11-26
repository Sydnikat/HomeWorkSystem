using HWS.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IAssignmentService
    {
        Task<ICollection<Assignment>> GetAssignmentsForStudent(User student);

        Task<ICollection<Assignment>> GetAssignmentsForTeacher(User teacher);

        Task<Assignment> GetAssignment(Guid id);

        Task<bool> GradeAssignment(User grader, Assignment assignment, string grade);

        Task<bool> ReserveAssignment(User grader, Assignment assignment);

        Task<bool> FreeAssignment(User grader, Assignment assignment);

        Task<Assignment> ChangeAssignmentFile(Assignment assignment, string fileName, IFormFile file);

        Task<MemoryStream> GetFile(Assignment assignment);
    }
}
