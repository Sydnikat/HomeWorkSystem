import { combineReducers } from "@reduxjs/toolkit";
import { assignmentReducer } from "./assignmentStore";
import { userReducer } from "./userStore";
import { groupReducer } from "./groupStore";
import { commentReducer } from "./commentStore";

export const rootReducer = combineReducers({
  assignmentReducer,
  userReducer,
  groupReducer,
  commentReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
