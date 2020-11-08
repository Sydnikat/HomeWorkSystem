import axios, { AxiosResponse } from "axios";
import { IAssignmentResponse } from "../models/assignment";

export interface AssignmentService {
  getAssignments(): Promise<IAssignmentResponse[]>;
  upload(assignmentId: string): Promise<void>;
}

export const assignmentService: AssignmentService = {
  async getAssignments() {
    return await axios
      .get("url")
      .then((res: AxiosResponse) => {
        if (res.status === 200) {
          return res.data as IAssignmentResponse[];
        }
        return exampleAssignments;
      })
      .catch((err) => {
        console.log(err);
        return exampleAssignments;
      });
  },

  async upload(assignmentId: string) {
    console.log(assignmentId);
    return await axios
      .post("url")
      .then((res: AxiosResponse) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
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
];
