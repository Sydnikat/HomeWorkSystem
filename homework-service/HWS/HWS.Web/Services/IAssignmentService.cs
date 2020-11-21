using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IAssignmentService
    {
        Task<ICollection<Assignment>> GetAssignmentsForStudent(User student);

        Task<ICollection<Assignment>> GetAssignmentsForTeacher(User teacher);
    }
}
