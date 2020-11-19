export interface ICommentResponse {
  id: string;
  createdBy: string;
  creationDate: string;
  content: string;
}

export interface ICommentRequest {
  content: string;
}
