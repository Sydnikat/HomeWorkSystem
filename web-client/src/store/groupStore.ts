import {IGroupResponse} from "../models/group";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";

export interface GroupState {
  groups: IGroupResponse[];
}

const initialGroupsState: GroupState = {
  groups: []
};

export const group = createSlice({
  name: "group",
  initialState: initialGroupsState,
  reducers: {
    setGroups(
      state: GroupState,
      action: PayloadAction<IGroupResponse[]>
    ) {
      state.groups = action.payload;
    },

    addNewGroup(
      state: GroupState,
      action: PayloadAction<IGroupResponse>
    ) {
      state.groups = [action.payload, ...state.groups];
    }
  }
});

export const {
  setGroups,
  addNewGroup
} = group.actions;
export const groupReducer = group.reducer;