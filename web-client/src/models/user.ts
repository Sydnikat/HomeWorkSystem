export interface IUser {
  id: string;
  userName: string;
  userFullName: string;
  role: string;
  accessToken: string;
  refreshToken: string;
}

export interface IUserResponse {
  id: string;
  userName: string;
  userFullName: string;
  role: string;
}
