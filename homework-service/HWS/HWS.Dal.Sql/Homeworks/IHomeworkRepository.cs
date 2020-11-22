using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Homeworks
{
    public interface IHomeworkRepository
    {
        Task<Homework> FindById(Guid id);

        Task<Assignment> InsertAssignment(Guid homeworkId, Assignment assignment);

        Task<Comment> InsertComment(User user, Homework homework, Comment comment);
    }
}
