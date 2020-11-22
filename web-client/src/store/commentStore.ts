import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ICommentResponse } from "../models/comment";

export interface CommentState {
  comments: ICommentResponse[];
}

const initialCommentsState: CommentState = {
  comments: [],
};

export const comment = createSlice({
  name: "comment",
  initialState: initialCommentsState,
  reducers: {
    setComments(
      state: CommentState,
      action: PayloadAction<ICommentResponse[]>
    ) {
      state.comments = action.payload;
    },

    addNewComment(
      state: CommentState,
      action: PayloadAction<ICommentResponse>
    ) {
      state.comments = [...state.comments, action.payload];
    },
  },
});

export const { setComments, addNewComment } = comment.actions;
export const commentReducer = comment.reducer;
