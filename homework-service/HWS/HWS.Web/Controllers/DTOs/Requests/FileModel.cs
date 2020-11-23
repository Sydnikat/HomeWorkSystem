using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Controllers.DTOs.Requests
{
    public class FileModel
    {
        [Required]
        [MinLength(length: 3)]
        public string FileName { get; set; }

        [Required]
        public IFormFile FormFile { get; set; }
    }
}
