import { AxiosError } from "axios";
import { ICommentRequest, ICommentResponse } from "../models/comment";
import { axiosInstance } from "./config/axios";
import { homeworkServiceUrl } from "./config/url";
import {IAssignmentResponse} from "../models/assignment";
import {AxiosError} from "axios";

interface HomeworkService {
  getComments(homeworkId: string): Promise<ICommentResponse[]>;
  sendComment(
    homeworkId: string,
    comment: ICommentRequest
  ): Promise<ICommentResponse | null>;
  createAssignment(homeworkId: string): Promise<IAssignmentResponse | null>;
}

export const homeworkService: HomeworkService = {
  async getComments(homeworkId: string) {
    try {
      const response = await axiosInstance.get(
        `${homeworkServiceUrl}/${homeworkId}/comments`
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

  async sendComment(homeworkId: string, comment: ICommentRequest) {
    try {
      const response = await axiosInstance.post(
        `${homeworkServiceUrl}/${homeworkId}/comments`,
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

  async createAssignment(homeworkId: string) {
    try {
      const response = await axiosInstance.post(
        `${homeworkServiceUrl}/${homeworkId}`,
        {}
      );
      if (response.status === 201) {
        return response.data as IAssignmentResponse;
      }
      return null;
    }  catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return null;
    }
  },
};
