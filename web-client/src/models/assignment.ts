export interface IAssignmentResponse {
  id: string;
  homeworkId: string;
  groupId: string;
  grade: string;
  submissionDeadline: string;
  fileName?: string;
  userName: string;
  userFullName: string;
  homeworkTitle: string;
  groupName: string;
}
