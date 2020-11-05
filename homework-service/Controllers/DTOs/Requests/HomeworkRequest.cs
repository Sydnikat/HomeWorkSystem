using System;
using System.Collections.Generic;

namespace homework_service.Controllers.DTOs.Requests
{
    public class HomeworkRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxFileSize { get; set; }
        public string SubmissionDeadline { get; set; }
        public string SpplicationDeadline { get; set; }
        public int MaximumNumberOfStudents { get; set; }
        public IEnumerable<Guid> graders { get; set; }
        public IEnumerable<Guid> students { get; set; }
    }
}
