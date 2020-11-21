import { IAssignmentResponse } from "../models/assignment";
import { axiosInstance } from "./config/axios";
import { assignmentServiceUrl } from "./config/url";
import {AxiosError} from "axios";

export interface AssignmentService {
  getAssignments(): Promise<IAssignmentResponse[]>;
  grade(assignmentId: string, grade: string): Promise<void>;
  reserve(assignmentId: string): Promise<boolean>;
  free(assignmentId: string): Promise<boolean>;
  download(): string;
  upload(assignmentId: string): Promise<void>;
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
      console.log(response);
    } catch (err) {
      console.log(err);
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

  download() {
    return "download";
  },

  async upload(assignmentId: string) {
    try {
      const response = await axiosInstance.post(
        `${assignmentServiceUrl}/${assignmentId}/file`
      );
      console.log(response);
    } catch (err) {
      console.log(err);
    }
  },
};

const exampleAssignments: IAssignmentResponse[] = [
  {
    id: "a1",
    homeworkId: "h1",
    groupId: "g1",
    grade: "5",
    submissionDeadline: "2020. 11. 12.",
    fileName: "házifájlnév",
    userName: "hallgato1",
    userFullName: "Hallgató1",
    homeworkTitle: "Házi feladat beadó rendszer",
    groupName: "Csoport1",
    reservedBy: "8ab2c6a8-ab4a-42dc-97e2-668e273b5837",
    turnInDate: "2020. 11. 11.",
  },
  {
    id: "a2",
    homeworkId: "h2",
    groupId: "g2",
    grade: "Jeles. Szép munka! Ez egy hosszú értékelés.",
    submissionDeadline: "2020. 11. 12.",
    fileName: "házifájlnév2",
    userName: "hallgato1",
    userFullName: "Hallgató1",
    homeworkTitle: "Nagy házi feladat",
    groupName: "Csoport2",
  },
  {
    id: "a3",
    homeworkId: "h2",
    groupId: "g2",
    grade: "",
    submissionDeadline: "2020. 11. 12.",
    fileName: "házifájlnév3",
    userName: "hallgato3",
    userFullName: "Hallgató3",
    homeworkTitle: "Nagy házi feladat",
    groupName: "Csoport2",
  },
  {
    id: "a4",
    homeworkId: "h2",
    groupId: "g2",
    grade: "",
    submissionDeadline: "2020. 11. 12.",
    fileName: "házifájlnév4",
    userName: "hallgato4",
    userFullName: "Hallgató4",
    homeworkTitle: "Nagy házi feladat",
    groupName: "Csoport2",
  },
  {
    id: "a5",
    homeworkId: "h2",
    groupId: "g2",
    grade: "",
    submissionDeadline: "2020. 11. 12.",
    fileName: "házifájlnév5",
    userName: "hallgato5",
    userFullName: "Hallgató5",
    homeworkTitle: "Nagy házi feladat",
    groupName: "Csoport2",
    reservedBy: "8ab2c6a8-ab4a-42dc-97e2-668e273b5837",
  },
];
