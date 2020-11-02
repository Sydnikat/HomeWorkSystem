import { combineReducers } from "@reduxjs/toolkit";
import { userReducer } from "./userStore";

export const rootReducer = combineReducers({
  userReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
