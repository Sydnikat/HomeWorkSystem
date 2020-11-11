import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import FilterAssignmentPanel from "./FilterAssignmentsPanel";
import { useAssignments } from "../shared/hooks";
import {
  setAssignments,
  setFilteredAssignments,
} from "../store/assignmentStore";
import FilteredAssignments from "./FilteredAssignments";

const Grading: React.FC = () => {
  const dispatch = useDispatch();
  const assignments = useAssignments();

  useEffect(() => {
    dispatch(setAssignments(assignments));
    dispatch(
      setFilteredAssignments(
        assignments.filter((a) => a.reservedBy === undefined)
      )
    );
  }, [assignments, dispatch]);

  return (
    <div className="mt-5">
      <FilterAssignmentPanel />
      <FilteredAssignments />
    </div>
  );
};

export default Grading;
