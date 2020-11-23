import { IAssignmentResponse } from "../models/assignment";
import { axiosInstance } from "./config/axios";
import { assignmentServiceUrl } from "./config/url";
import { AxiosError } from "axios";

export interface AssignmentService {
  getAssignments(): Promise<IAssignmentResponse[]>;
  grade(assignmentId: string, grade: string): Promise<boolean>;
  reserve(assignmentId: string): Promise<boolean>;
  free(assignmentId: string): Promise<boolean>;
  download(assignment: IAssignmentResponse): Promise<void>;
  upload(assignmentId: string, file: FormData): Promise<string>;
}

export const assignmentService: AssignmentService = {
  async getAssignments() {
    try {
      const response = await axiosInstance.get(`${assignmentServiceUrl}`);
      if (response.status === 200) {
        return response.data as IAssignmentResponse[];
      }
      return [];
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return [];
    }
  },

  async grade(assignmentId: string, grade: string) {
    try {
      const response = await axiosInstance.post(
        `${assignmentServiceUrl}/${assignmentId}/grade`,
        { grade }
      );
      return response.status === 200;
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return false;
    }
  },

  async reserve(assignmentId: string) {
    try {
      const response = await axiosInstance.post(
        `${assignmentServiceUrl}/${assignmentId}/reserve`
      );
      if (response.status === 200) {
        return true;
      }
      return false;
    } catch (err) {
      console.log(err);
      return false;
    }
  },

  async free(assignmentId: string) {
    try {
      const response = await axiosInstance.post(
        `${assignmentServiceUrl}/${assignmentId}/free`
      );
      if (response.status === 200) {
        return true;
      }
      return false;
    } catch (err) {
      console.log(err);
      return false;
    }
  },

  async download(assignment: IAssignmentResponse) {
    try {
      const response = await axiosInstance.get(
        `${assignmentServiceUrl}/${assignment.id}/file`,
        { method: "GET", responseType: "blob" }
      );
      if (response.status === 200) {
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute(
          "download",
          `${assignment.fileName ?? "file"}`
        );
        document.body.appendChild(link);
        link.click();
        link.remove();
      }
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
    }
  },

  async upload(assignmentId: string, file: FormData) {
    try {
      const response = await axiosInstance.post(
        `${assignmentServiceUrl}/${assignmentId}/file`, file
      );
      if (response.status === 200) {
        return response.data as string;
      }
      return "";
    } catch (err) {
      console.log(err);
      console.log((err as AxiosError)?.response?.data);
      return "";
    }
  },
};
