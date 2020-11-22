import { ICommentRequest, ICommentResponse } from "../models/comment";
import { IGroupRequest, IGroupResponse } from "../models/group";
import { IHomeworkRequest, IHomeworkResponse } from "../models/homework";
import { axiosInstance } from "./config/axios";
import { groupServiceUrl } from "./config/url";
import { AxiosError } from "axios";

export interface GroupService {
  getGroups(): Promise<IGroupResponse[]>;
  createGroup(group: IGroupRequest): Promise<IGroupResponse | null>;
  getComments(groupId: string): Promise<ICommentResponse[]>;
  sendComment(
    groupId: string,
    comment: ICommentRequest
  ): Promise<ICommentResponse | null>;
  joinGroup(code: string): Promise<IGroupResponse | null>;
  createHomework(
    groupId: string,
    homework: IHomeworkRequest
  ): Promise<IHomeworkResponse | null>;
}

export const groupService: GroupService = {
  async getGroups() {
    try {
      const response = await axiosInstance.get(`${groupServiceUrl}`);
      if (response.status === 200) {
        return response.data as IGroupResponse[];
      }
      return [];
    } catch (err) {
      console.log(err);
      return [];
    }
  },

  async createGroup(request: IGroupRequest) {
    try {
      const response = await axiosInstance.post(`${groupServiceUrl}`, request);
      if (response.status === 201) {
        return response.data as IGroupResponse;
      }
      return null;
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return null;
    }
  },

  async getComments(groupId: string) {
    try {
      const response = await axiosInstance.get(
        `${groupServiceUrl}/${groupId}/comments`
      );
      if (response.status === 200) {
        return response.data as ICommentResponse[];
      }
      return [];
    } catch (err) {
      console.log(err);
      return [];
    }
  },

  async sendComment(groupId: string, comment: ICommentRequest) {
    try {
      const response = await axiosInstance.post(
        `${groupServiceUrl}/${groupId}/comments`,
        comment
      );
      if (response.status === 201) {
        return response.data as ICommentResponse;
      }
      return null;
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return null;
    }
  },

  async joinGroup(code: string) {
    try {
      const response = await axiosInstance.post(`${groupServiceUrl}/join`, {
        code,
      });
      if (response.status === 200) {
        return response.data as IGroupResponse;
      }
      return null;
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return null;
    }
  },

  async createHomework(groupId: string, homework: IHomeworkRequest) {
    try {
      const response = await axiosInstance.post(
        `${groupServiceUrl}/${groupId}/homeworks`,
        homework
      );
      if (response.status === 201) {
        return response.data as IHomeworkResponse;
      }

      return null;
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return null;
    }
  },
};
