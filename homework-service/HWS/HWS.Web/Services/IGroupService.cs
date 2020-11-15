using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IGroupService
    {
        Task<Group> GetGroup(Guid id);

        Task<Group> CreateGroup(Group newGroup);
    }
}
