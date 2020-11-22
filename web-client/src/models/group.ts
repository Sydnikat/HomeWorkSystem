import { IHomeworkResponse } from "./homework";
import {IUserResponse} from "./user";

export interface IGroupResponse {
  id: string;
  name: string;
  code: string;
  ownerId: string;
  ownerFullName: string;
  students: IUserResponse[];
  teachers: IUserResponse[];
  homeworks: IHomeworkResponse[];
}

export interface IGroupRequest {
  name: string;
  students: string[];
  teachers: string[];
}
