using HWS.Domain;
using System;

namespace HWS.Controllers.DTOs.Responses
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string Content { get; set; }

        public CommentResponse(Comment comment)
        {
            Id = comment.Id;
            CreationDate = comment.CreationDate;
            CreatedBy = comment.CreatedBy;
            Content = comment.Content;
        }
    }
}
