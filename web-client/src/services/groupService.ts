import { AxiosResponse } from "axios";
import { ICommentRequest, ICommentResponse } from "../models/comment";
import { IGroupRequest, IGroupResponse } from "../models/group";
import { IHomeworkRequest } from "../models/homework";
import { axiosInstance } from "./config/axios";
import { groupServiceUrl } from "./config/url";

export interface GroupService {
  getGroups(): Promise<IGroupResponse[]>;
  createGroup(group: IGroupRequest): Promise<void>;
  getComments(groupId: string): Promise<ICommentResponse[]>;
  sendComment(groupId: string, comment: ICommentRequest): Promise<void>;
  joinGroup(groupId: string, code: string): Promise<void>;
  createHomework(groupId: string, homework: IHomeworkRequest): Promise<void>;
}

export const groupService: GroupService = {
  async getGroups() {
    return await axiosInstance
      .get(`${groupServiceUrl}`)
      .then((res: AxiosResponse) => {
        if (res.status === 200) {
          return res.data as IGroupResponse[];
        }
        return exampleGroups;
      })
      .catch((err) => {
        console.log(err);
        return exampleGroups;
      });
  },

  async createGroup(group: IGroupRequest) {
    return await axiosInstance
      .post(`${groupServiceUrl}`, { group })
      .then((res: AxiosResponse) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  },

  async getComments(groupId: string) {
    return await axiosInstance
      .get(`${groupServiceUrl}/${groupId}/comments`)
      .then((res: AxiosResponse) => {
        if (res.status === 200) {
          return res.data as ICommentResponse[];
        }
        return exampleComments;
      })
      .catch((err) => {
        console.log(err);
        return exampleComments;
      });
  },

  async sendComment(groupId: string, comment: ICommentRequest) {
    return await axiosInstance
      .post(`${groupServiceUrl}/${groupId}/comments`, { comment })
      .then((res: AxiosResponse) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  },

  async joinGroup(groupId: string, code: string) {
    return await axiosInstance
      .post(`${groupServiceUrl}/${groupId}`, { code })
      .then((res: AxiosResponse) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  },

  async createHomework(groupId: string, homework: IHomeworkRequest) {
    return await axiosInstance
      .post(`${groupServiceUrl}/${groupId}/homework`, { homework })
      .then((res: AxiosResponse) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  },
};

const exampleGroups: IGroupResponse[] = [
  {
    id: "g1",
    name: "Csoport1",
    code: "abc123",
    ownerId: "8ab2c6a8-ab4a-42dc-97e2-668e273b5837",
    ownerFullName: "Oktató1",
    students: ["Hallgató1", "Hallgató2"],
    teachers: ["Oktató1", "Oktató2"],
    homeworks: [
      {
        id: "h1",
        title: "Házi feladat beadó rendszer",
        description: "Többfelhasználós rendszer",
        maxFileSize: 50,
        groupId: "g1",
        submissionDeadline: "2020. 11. 12.",
        applicationDeadline: "2020. 10. 15.",
        maximumNumberOfStudents: 2,
        currentNumberOfStudents: 2,
        graders: ["Oktató1", "Oktató2"],
        students: ["Hallgató1", "Hallgató2"],
      },
      {
        id: "h3",
        title: "Szorgalmi házi feladat",
        description:
          "Extra feladat plusz pontért. Ez egy hosszú leírása a házi feladatnak, több sorban is kifér.",
        maxFileSize: 50,
        groupId: "g1",
        submissionDeadline: "2020. 11. 20.",
        maximumNumberOfStudents: 2,
        currentNumberOfStudents: 2,
        graders: ["Oktató1", "Oktató2"],
        students: ["Hallgató2"],
      },
      {
        id: "h4",
        title: "Második szorgalmi házi feladat",
        description:
          "Extra feladat plusz pontért. Ez egy hosszú leírása a házi feladatnak, több sorban is kifér.",
        maxFileSize: 50,
        groupId: "g1",
        submissionDeadline: "2020. 11. 27.",
        applicationDeadline: "2020. 10. 15.",
        maximumNumberOfStudents: 2,
        currentNumberOfStudents: 2,
        graders: ["Oktató1", "Oktató2"],
        students: ["Hallgató2"],
      },
    ],
  },
  {
    id: "g2",
    name: "Csoport2",
    code: "abc456",
    ownerId: "ownerid2",
    ownerFullName: "Oktató2",
    students: ["Hallgató1", "Hallgató2"],
    teachers: ["Oktató1", "Oktató2"],
    homeworks: [
      {
        id: "h2",
        title: "Nagy házi",
        description: "Nagy házi leírás",
        maxFileSize: 50,
        groupId: "g2",
        submissionDeadline: "2020. 11. 12.",
        applicationDeadline: "2020. 10. 15.",
        maximumNumberOfStudents: 20,
        currentNumberOfStudents: 2,
        graders: ["Oktató1", "Oktató2"],
        students: ["Hallgató1", "Hallgató2"],
      },
    ],
  },
];

const exampleComments: ICommentResponse[] = [
  {
    id: "c1",
    createdBy: "hallgato1",
    creationDate: "2020. 11. 16.",
    content: "Mikor lesz a ZH?",
  },
  {
    id: "c2",
    createdBy: "hallgato2",
    creationDate: "2020. 11. 17.",
    content: "Holnap.",
  },
];
