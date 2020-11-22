import { combineReducers } from "@reduxjs/toolkit";
import { assignmentReducer } from "./assignmentStore";
import { userReducer } from "./userStore";
import {groupReducer} from "./groupStore";

export const rootReducer = combineReducers({
  assignmentReducer,
  userReducer,
  groupReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
