using HWS.Dal.Sql.Homeworks;
using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository homeworkRepository;

        public HomeworkService(IHomeworkRepository homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        public async Task<Homework> GetHomework(Guid id)
        {
            return await homeworkRepository.FindById(id);
        }

        public async Task<Assignment> CreateHomework(Homework homework, Assignment newAssignment)
        {
            return await homeworkRepository.InsertAssignment(homework.Id, newAssignment).ConfigureAwait(false);
        }

        public async Task<Comment> CreateComment(User user, Homework homework, string content)
        {
            return await homeworkRepository.InsertComment(user, homework, content);
        }
    }
}
