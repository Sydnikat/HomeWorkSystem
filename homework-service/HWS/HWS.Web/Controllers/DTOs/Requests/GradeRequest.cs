using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Controllers.DTOs.Requests
{
    public class GradeRequest
    {
        public GradeRequest()
        {
        }

        [Required]
        public string Grade { get; set; }
    }
}
