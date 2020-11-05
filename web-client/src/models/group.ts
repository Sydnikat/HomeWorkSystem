import { IHomeworkResponse } from "./homework";

export interface IGroupResponse {
  id: string;
  name: string;
  code: string;
  ownerId: string;
  ownerFullName: string;
  students: string[];
  teachers: string[];
  homeworks: IHomeworkResponse[];
}

export interface IGroupRequest {
  name: string;
  students: string[];
  teachers: string[];
}
