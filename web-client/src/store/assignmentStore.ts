import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IAssignmentResponse } from "../models/assignment";

export interface AssignmentState {
  assignments: IAssignmentResponse[];
  filteredAssignments: IAssignmentResponse[];
}

const initialAssignmentState: AssignmentState = {
  assignments: [],
  filteredAssignments: [],
};

export const assignment = createSlice({
  name: "assignment",
  initialState: initialAssignmentState,
  reducers: {
    setAssignments(
      state: AssignmentState,
      action: PayloadAction<IAssignmentResponse[]>
    ) {
      state.assignments = action.payload;
    },

    setFilteredAssignments(
      state: AssignmentState,
      action: PayloadAction<IAssignmentResponse[]>
    ) {
      state.filteredAssignments = action.payload;
    },

    freeAssignment(state: AssignmentState, action: PayloadAction<string>) {
      const assignmentId = action.payload;
      const index = state.assignments.findIndex((a) => a.id === assignmentId);

      if (index !== -1) {
        const assignment = state.assignments[index];
        const newAssignment: IAssignmentResponse = {
          ...assignment,
          reservedBy: undefined,
        };
        state.assignments[index] = newAssignment;
      }
    },

    reserveAssignment(
      state: AssignmentState,
      action: PayloadAction<{ assignmentId: string; userId: string }>
    ) {
      const { assignmentId, userId } = action.payload;
      const index = state.assignments.findIndex((a) => a.id === assignmentId);

      if (index !== -1) {
        const assignment = state.assignments[index];
        const newAssignment: IAssignmentResponse = {
          ...assignment,
          reservedBy: userId,
        };
        state.assignments[index] = newAssignment;
      }
    },

    gradeAssignment(
      state: AssignmentState,
      action: PayloadAction<{ assignmentId: string; grade: string }>
    ) {
      const { assignmentId, grade } = action.payload;
      const index = state.assignments.findIndex((a) => a.id === assignmentId);

      if (index !== -1) {
        const assignment = state.assignments[index];
        const newAssignment: IAssignmentResponse = {
          ...assignment,
          grade: grade,
        };
        state.assignments[index] = newAssignment;
      }
    },

    addAssignment(
      state: AssignmentState,
      action: PayloadAction<IAssignmentResponse>
    ) {
      state.assignments = [...state.assignments, action.payload];
    },
  },
});

export const {
  setAssignments,
  addAssignment,
  setFilteredAssignments,
  freeAssignment,
  reserveAssignment,
  gradeAssignment,
} = assignment.actions;
export const assignmentReducer = assignment.reducer;
