using HWS.Domain;
using System;
using System.Collections.Generic;

#nullable enable

namespace HWS.Controllers.DTOs.Requests
{
    public class HomeworkRequest
    {
        public HomeworkRequest()
        {
        }

        public HomeworkRequest(
            string title,
            string description,
            int maxFileSize,
            DateTime submissionDeadline,
            DateTime? applicationDeadline,
            int maximumNumberOfStudents,
            ICollection<Guid> graders,
            ICollection<Guid> students)
        {
            Title = title;
            Description = description;
            MaxFileSize = maxFileSize;
            SubmissionDeadline = submissionDeadline;
            ApplicationDeadline = applicationDeadline;
            MaximumNumberOfStudents = maximumNumberOfStudents;
            this.Graders = graders;
            this.Students = students;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxFileSize { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public int MaximumNumberOfStudents { get; set; }
        public ICollection<Guid> Graders { get; set; }
        public ICollection<Guid> Students { get; set; }

        public Homework ToNew()
        {
            return new Homework(
                _id: 0,
                id: Guid.Empty,
                title: Title,
                description: Description,
                maxFileSize: MaxFileSize,
                group: null,
                submissionDeadline: SubmissionDeadline,
                applicationDeadline: ApplicationDeadline ?? DateTime.MaxValue,
                maximumNumberOfStudents: MaximumNumberOfStudents,
                currentNumberOfStudents: Students.Count,
                graders: new List<User>(),
                students: new List<User>(),
                comments: new List<Comment>(),
                assignments: new List<Assignment>()
                );
        }
    }
}
