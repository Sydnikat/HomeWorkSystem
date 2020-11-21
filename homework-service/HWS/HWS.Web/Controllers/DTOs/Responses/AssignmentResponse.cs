using HWS.Domain;
using System;

#nullable enable

namespace HWS.Controllers.DTOs.Responses
{
    public class AssignmentResponse
    {
        public Guid Id { get; }
        public Guid HomeWorkId { get; }
        public Guid GroupId { get; }
        public string Grade { get; }
        public DateTime? TurnInDate { get; }
        public Guid? ReservedBy { get; }
        public DateTime SubmissionDeadline { get; }
        public string? FileName { get; }
        public string UserFullName { get; }
        public string UserName { get; }
        public string HomeworkTitle { get; }
        public string GroupName { get; }

        public AssignmentResponse(
            Guid id,
            Guid homeWorkId,
            Guid groupId,
            string grade,
            DateTime? turnInDate,
            Guid? reservedBy,
            DateTime submissionDeadline, 
            string? fileName,
            string userFullName, 
            string userName,
            string homeworkTitle, 
            string groupName)
        {
            Id = id;
            HomeWorkId = homeWorkId;
            GroupId = groupId;
            Grade = grade;
            TurnInDate = turnInDate;
            ReservedBy = reservedBy;
            SubmissionDeadline = submissionDeadline;
            FileName = fileName;
            UserFullName = userFullName;
            UserName = userName;
            HomeworkTitle = homeworkTitle;
            GroupName = groupName;
        }

        public static AssignmentResponse Of(Assignment assignment)
        {
            DateTime? turnInDate = assignment.TurnInDate;
            if (assignment.SubmissionDeadline < assignment.TurnInDate)
            {
                turnInDate = null;
            }

            Guid? reservedBy = assignment.ReservedBy.Id;
            if (reservedBy == Guid.Empty)
            {
                reservedBy = null;
            }

            return new AssignmentResponse(
                id: assignment.Id,
                homeWorkId: assignment.Homework.Id,
                groupId: assignment.Group.Id,
                grade: assignment.Grade,
                turnInDate: turnInDate,
                reservedBy: reservedBy,
                submissionDeadline: assignment.SubmissionDeadline,
                fileName: assignment.FileName == "" ? null : assignment.FileName,
                userFullName: assignment.Student.UserFullName,
                userName: assignment.Student.UserName,
                homeworkTitle: assignment.Homework.Title,
                groupName: assignment.Group.Name
                );
        }
    }
}
