using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWS.Controllers.DTOs.Responses
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
        public IEnumerable<UserResponse> Graders { get; set; }
        public IEnumerable<UserResponse> Students { get; set; }

        public HomeworkResponse() { }

        public static HomeworkResponse ForTeacher(Homework homework)
        {
            return new HomeworkResponse()
            {
                Id = homework.Id,
                Title = homework.Title,
                Description = homework.Description,
                MaxFileSize = homework.MaxFileSize,
                GroupId = homework.Group.Id,
                SubmissionDeadline = homework.SubmissionDeadline,
                ApplicationDeadline = homework.ApplicationDeadline,
                MaximumNumberOfStudents = homework.MaximumNumberOfStudents,
                CurrentNumberOfStudents = homework.Students.Count,
                Graders = homework.Graders.Select(g => new UserResponse(g)),
                Students = homework.Students.Select(s => new UserResponse(s))
            };
        }

        public static HomeworkResponse ForStudent(Homework homework)
        {
            return new HomeworkResponse()
            {
                Id = homework.Id,
                Title = homework.Title,
                Description = homework.Description,
                MaxFileSize = homework.MaxFileSize,
                GroupId = homework.Group.Id,
                SubmissionDeadline = homework.SubmissionDeadline,
                ApplicationDeadline = homework.ApplicationDeadline,
                MaximumNumberOfStudents = homework.MaximumNumberOfStudents,
                CurrentNumberOfStudents = homework.Students.Count,
                Graders = homework.Graders.Select(g => new UserResponse(g)),
                Students = new List<UserResponse>()
            };
        }
    }
}
