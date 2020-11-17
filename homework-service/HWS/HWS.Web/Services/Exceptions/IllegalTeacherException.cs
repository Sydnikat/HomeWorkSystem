using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class IllegalTeacherException : Exception
    {
        public IllegalTeacherException(string message) : base(message)
        {
        }
    }
}
