export interface IHomeworkResponse {
  id: string;
  title: string;
  description: string;
  maxFileSize: number;
  groupId: string;
  submissionDeadline: string;
  applicationDeadline?: string;
  maximumNumberOfStudents: number;
  currentNumberOfStudents: number;
  graders: string[];
  students: string[];
}

export interface IHomeworkRequest {
  title: string;
  description: string;
  maxFileSize: number;
  groupId: string;
  submissionDeadline: string;
  applicationDeadline?: string;
  maximumNumberOfStudents: number;
  graders: string[];
  students: string[];
}
