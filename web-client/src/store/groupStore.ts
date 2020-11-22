import { IGroupResponse } from "../models/group";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IHomeworkResponse } from "../models/homework";

export interface GroupState {
  groups: IGroupResponse[];
}

const initialGroupsState: GroupState = {
  groups: [],
};

export const group = createSlice({
  name: "group",
  initialState: initialGroupsState,
  reducers: {
    setGroups(state: GroupState, action: PayloadAction<IGroupResponse[]>) {
      state.groups = action.payload;
    },

    addNewGroup(state: GroupState, action: PayloadAction<IGroupResponse>) {
      state.groups = [action.payload, ...state.groups];
    },

    updateGroupHomeworks(
      state: GroupState,
      action: PayloadAction<IHomeworkResponse>
    ) {
      const homework = action.payload;
      const index = state.groups.findIndex((g) => g.id === homework.groupId);

      if (index !== -1) {
        state.groups[index].homeworks = [
          ...state.groups[index].homeworks,
          homework,
        ];
      }
    },
  },
});

export const { setGroups, addNewGroup, updateGroupHomeworks } = group.actions;
export const groupReducer = group.reducer;
