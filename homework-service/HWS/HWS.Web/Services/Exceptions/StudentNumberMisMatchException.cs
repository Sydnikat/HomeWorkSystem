using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class StudentNumberMisMatchException : Exception
    {
        public StudentNumberMisMatchException(string message) : base(message)
        {
        }
    }
}
