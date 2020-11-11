import { combineReducers } from "@reduxjs/toolkit";
import { assignmentReducer } from "./assignmentStore";
import { userReducer } from "./userStore";

export const rootReducer = combineReducers({
  assignmentReducer,
  userReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
