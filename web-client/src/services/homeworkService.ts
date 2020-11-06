import axios, { AxiosResponse } from "axios";
import { ICommentResponse } from "../models/comment";

interface HomeworkService {
  getComments(homeworkId: string): Promise<ICommentResponse[]>;
  sendComment(homeworkId: string, comment: string): Promise<void>;
}

export const homeworkService: HomeworkService = {
  async getComments(homeworkId: string) {
    console.log(homeworkId);
    return await axios
      .get("url")
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

  async sendComment(homeworkId: string, comment: string) {
    console.log(homeworkId, comment);
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
