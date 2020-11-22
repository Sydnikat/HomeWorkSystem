import { AxiosError } from "axios";
import { ICommentRequest, ICommentResponse } from "../models/comment";
import { axiosInstance } from "./config/axios";
import { homeworkServiceUrl } from "./config/url";

interface HomeworkService {
  getComments(homeworkId: string): Promise<ICommentResponse[]>;
  sendComment(
    homeworkId: string,
    comment: ICommentRequest
  ): Promise<ICommentResponse | null>;
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
      return exampleComments;
    } catch (err) {
      console.log(err);
      return exampleComments;
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
