using HWS.Domain;
using System;

namespace HWS.Controllers.DTOs.Responses
{
    public class CommentResponse
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }
        public string CreatedBy { get; }
        public string Content { get; }

        public CommentResponse(Comment comment)
        {
            Id = comment.Id;
            CreationDate = comment.CreationDate;
            CreatedBy = comment.CreatedBy;
            Content = comment.Content;
        }
    }
}
