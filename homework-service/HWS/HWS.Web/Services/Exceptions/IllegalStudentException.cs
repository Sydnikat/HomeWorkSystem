using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class IllegalStudentException : Exception
    {
        public IllegalStudentException(string message) : base(message)
        {
        }
    }
}
