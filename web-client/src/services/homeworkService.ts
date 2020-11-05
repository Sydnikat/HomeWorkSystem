import { ICommentResponse } from "../models/comment";

interface HomeworkService {
  getComments(homeworkId: string): ICommentResponse[];
}

export const homeworkService: HomeworkService = {
  getComments(homeworkId: string) {
    console.log("call homework api with homeworkId", homeworkId);
    return exampleComments;
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
