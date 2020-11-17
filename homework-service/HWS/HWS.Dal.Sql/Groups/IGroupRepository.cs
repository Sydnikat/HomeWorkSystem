using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Groups
{
    public interface IGroupRepository
    {
        Task<IReadOnlyCollection<Group>> FindAllByInStudents(User student);

        Task<IReadOnlyCollection<Group>> FindAllByInTeachersOrIsOwner(User teacher);

        Task<Group> FindById(Guid id);

        Task<Group> Insert(Group group);

        Task<Homework> InsertHomework(Guid groupId, Homework homework);
    }
}
