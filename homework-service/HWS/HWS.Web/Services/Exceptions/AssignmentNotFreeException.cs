using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class AssignmentNotFreeException : Exception
    {
        public AssignmentNotFreeException(string message) : base(message)
        {
        }
    }
}
