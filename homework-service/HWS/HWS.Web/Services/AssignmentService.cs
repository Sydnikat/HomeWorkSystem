using HWS.Dal.Sql.Assignments;
using HWS.Domain;
using HWS.Services.Config;
using HWS.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IFileSettings settings;

        public AssignmentService(IAssignmentRepository assignmentRepository, IFileSettings settings)
        {
            this.assignmentRepository = assignmentRepository;
            this.settings = settings;
        }

        public async Task<Assignment> GetAssignment(Guid id)
        {
            return await assignmentRepository.FindById(id).ConfigureAwait(false);
        }

        public async Task<bool> GradeAssignment(User grader, Assignment assignment, string grade)
        {
            if (!canUserManageAssignment(grader, assignment))
                throw new IllegalTeacherException("Teacher cannot grade this assignment");

            if (assignment.ReservedBy.Id == Guid.Empty)
                throw new AssignmentNotReservedException("assignment is not reserved yet");

            if (assignment.ReservedBy.Id != grader.Id)
                throw new AssignmentNotFreeException($"assignment is already reserved by ${assignment.ReservedBy}");

            return await assignmentRepository.UpdateGrade(assignment.Id, grade).ConfigureAwait(false);
        }

        public async Task<bool> ReserveAssignment(User grader, Assignment assignment)
        {
            if (!canUserManageAssignment(grader, assignment))
                throw new IllegalTeacherException("Teacher cannot reserve this assignment");

            if (assignment.ReservedBy.Id != Guid.Empty)
                throw new AssignmentNotFreeException($"assignment is already reserved by ${assignment.ReservedBy}");

            return await assignmentRepository.UpdateReservedBy(assignment.Id, grader.Id).ConfigureAwait(false);
        }

        public async Task<bool> FreeAssignment(User grader, Assignment assignment)
        {
            if (!canUserManageAssignment(grader, assignment))
                throw new IllegalTeacherException("Teacher cannot free this assignment");

            if (assignment.ReservedBy.Id == Guid.Empty)
                return true;

            if (assignment.ReservedBy.Id != grader.Id)
                throw new AssignmentNotFreeException("Teacher cannot free this assignment reserved by someone else");

            return await assignmentRepository.UpdateReservedBy(assignment.Id, Guid.Empty).ConfigureAwait(false);
        }

        public async Task<Assignment> ChangeAssignmentFile(Assignment assignment, string fileName, IFormFile file)
        {
            var timeStamp = DateTime.Now.ToString(settings.TimeStampFormat);
            var newFileName = timeStamp + fileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), settings.FileDirectory);
            var newFileId = Guid.NewGuid();
            var newTurnInDate = DateTime.Now;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (assignment.FileId != Guid.Empty)
                {
                    var oldFilePath = Path.Combine(path, assignment.FileId.ToString());
                    if (File.Exists(oldFilePath))
                        File.Delete(oldFilePath);
                }

                using (Stream stream = new FileStream(Path.Combine(path, newFileId.ToString()), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                throw new FileHandlingFailedException($"Saving the new file was unsuccessful. Cause: ${e.Message}");
            }

            var success = await assignmentRepository.UpdateFileName(assignment.Id, newFileName, newFileId, newTurnInDate).ConfigureAwait(false);

            if (success)
            {
                assignment.TurnInDate = newTurnInDate;
                assignment.FileName = newFileName;
                assignment.FileId = newFileId;
                return assignment;
            }
            else
                return null;
        }

        public async Task<MemoryStream> GetFile(Assignment assignment)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), settings.FileDirectory, assignment.FileId.ToString());
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return memory;
            }
            catch (Exception e)
            {
                throw new FileHandlingFailedException($"Saving the new file was unsuccessful. Cause: ${e.Message}");
            }
        }

        public async Task<ICollection<Assignment>> GetAssignmentsForStudent(User student)
        {
            return (await assignmentRepository.FindAllByStudent(student).ConfigureAwait(false)).ToList();
        }

        public async Task<ICollection<Assignment>> GetAssignmentsForTeacher(User teacher)
        {
            return (await assignmentRepository.FindAllByUserInGraders(teacher).ConfigureAwait(false)).ToList();
        }

        private bool canUserManageAssignment(User user, Assignment assignment)
            => assignment.Homework.Graders.FirstOrDefault(g => g.Id == user.Id) != null;
    }
}
