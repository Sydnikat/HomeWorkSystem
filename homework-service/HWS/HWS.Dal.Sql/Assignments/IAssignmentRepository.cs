using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Assignments
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> FindAllByStudent(User student);

        Task<IEnumerable<Assignment>> FindAllByUserInGraders(User grader);

        Task<Assignment> Insert(Assignment assignment);

        Task<IEnumerable<Assignment>> InsertAll(ICollection<Assignment> assignments);
    }
}
