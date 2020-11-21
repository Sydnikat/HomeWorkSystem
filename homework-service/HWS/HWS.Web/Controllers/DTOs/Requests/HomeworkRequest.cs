using HWS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MinLength(length: 3)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 50)]
        public int MaxFileSize { get; set; }

        [Required]
        public DateTime SubmissionDeadline { get; set; }

        public DateTime? ApplicationDeadline { get; set; }

        [Required]
        public int MaximumNumberOfStudents { get; set; }

        [Required]
        public ICollection<Guid> Graders { get; set; }

        [Required]
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
