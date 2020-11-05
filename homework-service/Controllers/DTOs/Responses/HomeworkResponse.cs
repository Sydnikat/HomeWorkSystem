using homework_service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_service.Controllers.DTOs.Responses
{
    public class HomeworkResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxFileSize { get; set; }
        public Guid GroupId { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int MaximumNumberOfStudents { get; set; }
        public int CurrentNumberOfStudents { get; set; }
        public IEnumerable<Guid> Graders { get; set; }
        public IEnumerable<Guid> Students { get; set; }

        public HomeworkResponse(Homework homework)
        {
            Id = homework.Id;
            Title = homework.Title;
            Description = homework.Description;
            MaxFileSize = homework.MaxFileSize;
            GroupId = homework.Group.Id;
            SubmissionDeadline = homework.SubmissionDeadline;
            ApplicationDeadline = homework.ApplicationDeadline;
            MaximumNumberOfStudents = homework.MaximumNumberOfStudents;
            CurrentNumberOfStudents = homework.Students.Count;
            Graders = homework.Graders.Select(g => g.Id);
            Students = homework.Students.Select(s => s.Id);
        }
    }
}
