using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class FileHandlingFailedException : Exception
    {
        public FileHandlingFailedException(string message) : base(message)
        {
        }
    }
}
