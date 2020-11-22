
using System.ComponentModel.DataAnnotations;

namespace HWS.Controllers.DTOs.Requests
{
    public class CommentRequest
    {
        [Required]
        [MinLength(length: 2)]
        [MaxLength(length: 255)]
        public string Content { get; set; }
    }
}
