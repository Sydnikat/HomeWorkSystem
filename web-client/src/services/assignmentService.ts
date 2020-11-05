import { IAssignmentResponse } from "../models/assignment";

interface AssignmentService {
  getAssignments(): IAssignmentResponse[];
}

export const assignmentService: AssignmentService = {
  getAssignments() {
    return [
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
  },
};
