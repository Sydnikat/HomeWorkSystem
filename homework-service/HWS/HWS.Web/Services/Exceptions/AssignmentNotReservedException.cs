﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Exceptions
{
    public class AssignmentNotReservedException : Exception
    {
        public AssignmentNotReservedException(string message) : base(message)
        {
        }
    }
}
