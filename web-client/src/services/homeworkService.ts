import { AxiosResponse } from "axios";
import { ICommentRequest, ICommentResponse } from "../models/comment";
import { axiosInstance } from "./config/axios";
import { homeworkServiceUrl } from "./config/url";

interface HomeworkService {
  getComments(homeworkId: string): Promise<ICommentResponse[]>;
  sendComment(homeworkId: string, comment: ICommentRequest): Promise<void>;
}

export const homeworkService: HomeworkService = {
  async getComments(homeworkId: string) {
    return await axiosInstance
      .get(`${homeworkServiceUrl}/${homeworkId}/comments`)
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

  async sendComment(homeworkId: string, comment: ICommentRequest) {
    return await axiosInstance
      .post(`${homeworkServiceUrl}/${homeworkId}/comments`, { comment })
      .then((res: AxiosResponse) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  },
};

const exampleComments: ICommentResponse[] = [
  {
    id: "c3",
    createdBy: "hallgato1",
    creationDate: "2020. 11. 16.",
    content: "Lesz házi megbeszélés?",
  },
  {
    id: "c4",
    createdBy: "hallgato2",
    creationDate: "2020. 11. 17.",
    content: "Igen, jövőhéten.",
  },
];
