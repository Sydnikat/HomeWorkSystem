using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class HomeworkIsFullException : Exception
    {
        public HomeworkIsFullException(string message) : base(message)
        {
        }
    }
}
