using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Homeworks
{
    public interface IHomeworkRepository
    {
        Task<Assignment> InsertAssignment(Guid homeworkId, Assignment assignment);

        Task<ICollection<Assignment>> InsertAllAssignment(Guid homeworkId, ICollection<Assignment> assignments);
    }
}
