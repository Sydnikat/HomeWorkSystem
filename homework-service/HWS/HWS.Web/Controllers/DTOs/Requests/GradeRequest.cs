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
        [MinLength(length: 2)]
        [MaxLength(length: 255)]
        public string Grade { get; set; }
    }
}
