using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IHomeworkService
    {
        Task<Homework> GetHomework(Guid id);

        Task<Assignment> CreateHomework(Homework homework, Assignment newAssignment);

        Task<Comment> CreateComment(User user, Homework homework, string content);
    }
}
