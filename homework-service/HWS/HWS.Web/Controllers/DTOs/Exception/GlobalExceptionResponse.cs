using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Controllers.DTOs.Exception
{
    public class GlobalExceptionResponse
    {
        public GlobalExceptionResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}
