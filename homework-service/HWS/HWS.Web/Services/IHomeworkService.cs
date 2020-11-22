﻿using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IHomeworkService
    {
        bool UserIsAppliedToHomework(User user, Homework homework);

        Task<Homework> GetHomework(Guid id);

        Task<Assignment> CreateAssignment(Homework homework, Assignment newAssignment);

        Task<Comment> CreateComment(User user, Homework homework, string content);
    }
}
