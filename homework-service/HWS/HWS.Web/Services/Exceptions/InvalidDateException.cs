using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(string message) : base(message)
        {
        }
    }
}
