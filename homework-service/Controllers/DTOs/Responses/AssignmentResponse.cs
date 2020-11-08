using HWS.Domain;
using System;

namespace HWS.Controllers.DTOs.Responses
{
    public class AssignmentResponse
    {
        public Guid Id { get; set; }
        public Guid HomeWorkId { get; set; }
        public Guid GroupId { get; set; }
        public string Grade { get; set; }
        public DateTime TurnInDate { get; set; }
        public Guid ReservedBy { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public string FileName { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public string HomeworkTitle { get; set; }
        public string GroupName { get; set; }

        public AssignmentResponse(Assignment assignment)
        {
            Id = assignment.Id;
            HomeWorkId = assignment.Homework.Id;
            GroupId = assignment.Group.Id;
            Grade = assignment.Grade;
            TurnInDate = assignment.TurnInDate;
            ReservedBy = assignment.ReservedBy.Id;
            SubmissionDeadline = assignment.SubmissionDeadline;
            FileName = assignment.FileName;
            UserName = assignment.Student.UserName;
            UserFullName = assignment.Student.UserFullName;
            HomeworkTitle = assignment.Homework.Title;
            GroupName = assignment.Group.Name;
        }
    }
}
