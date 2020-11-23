using HWS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace HWS.Controllers.DTOs.Responses
{
    public class HomeworkResponse
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public int MaxFileSize { get; }
        public Guid GroupId { get; }
        public DateTime SubmissionDeadline { get; }
        public DateTime? ApplicationDeadline { get; }
        public int MaximumNumberOfStudents { get; }
        public int CurrentNumberOfStudents { get; }
        public IEnumerable<string> Graders { get; }
        public IEnumerable<string> Students { get; }

        public HomeworkResponse(
            Guid id,
            string title,
            string description,
            int maxFileSize,
            Guid groupId,
            DateTime submissionDeadline,
            DateTime? applicationDeadline,
            int maximumNumberOfStudents, 
            int currentNumberOfStudents,
            IEnumerable<UserResponse> graders,
            IEnumerable<UserResponse> students)
        {
            Id = id;
            Title = title;
            Description = description;
            MaxFileSize = maxFileSize;
            GroupId = groupId;
            SubmissionDeadline = submissionDeadline;
            ApplicationDeadline = applicationDeadline;
            MaximumNumberOfStudents = maximumNumberOfStudents;
            CurrentNumberOfStudents = currentNumberOfStudents;
            Graders = graders.Select(g => g.UserFullName);
            Students = students.Select(s => s.UserFullName);
        }

        public static HomeworkResponse ForTeacher(Homework homework)
        {
            DateTime? applicationDeadline = homework.ApplicationDeadline;
            if (homework.SubmissionDeadline < applicationDeadline)
                applicationDeadline = null;

            return new HomeworkResponse(
                id: homework.Id,
                title: homework.Title,
                description: homework.Description,
                maxFileSize: homework.MaxFileSize,
                groupId: homework.Group.Id,
                submissionDeadline: homework.SubmissionDeadline,
                applicationDeadline: applicationDeadline,
                maximumNumberOfStudents: homework.MaximumNumberOfStudents,
                currentNumberOfStudents: homework.Students.Count,
                graders: homework.Graders.Select(g => new UserResponse(g)),
                students: homework.Students.Select(s => new UserResponse(s))
                );
        }

        public static HomeworkResponse ForStudent(Homework homework)
        {
            DateTime? applicationDeadline = homework.ApplicationDeadline;
            if (homework.SubmissionDeadline < applicationDeadline)
                applicationDeadline = null;

            return new HomeworkResponse(
                id: homework.Id,
                title: homework.Title,
                description: homework.Description,
                maxFileSize: homework.MaxFileSize,
                groupId: homework.Group.Id,
                submissionDeadline: homework.SubmissionDeadline,
                applicationDeadline: applicationDeadline,
                maximumNumberOfStudents: homework.MaximumNumberOfStudents,
                currentNumberOfStudents: homework.Students.Count,
                graders: homework.Graders.Select(g => new UserResponse(g)),
                students: new List<UserResponse>()
                ) ;
        }
    }
}
