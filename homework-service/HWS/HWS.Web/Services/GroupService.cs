using HWS.Dal.Sql.Groups;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<Group> CreateGroup(Group newGroup)
        {
            newGroup.Code = "asd123";

            return await groupRepository.Insert(newGroup);
        }

        public async Task<Group> GetGroup(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
