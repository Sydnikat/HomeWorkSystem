using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public interface IHomeworkService
    {
        Task<Assignment> CreateHomework(Homework homework, Assignment newAssignment);
    }
}
