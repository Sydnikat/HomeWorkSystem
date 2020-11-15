using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Groups
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> FindAllByStudent(User student);

        Task<Group> Insert(Group group);
    }
}
