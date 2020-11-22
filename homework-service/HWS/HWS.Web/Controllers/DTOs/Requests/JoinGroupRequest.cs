using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Controllers.DTOs.Requests
{
    public class JoinGroupRequest
    {
        public JoinGroupRequest()
        {
        }

        [Required]
        [MinLength(length: 8)]
        [MaxLength(length: 8)]
        public string Code { get; set; }
    }
}
