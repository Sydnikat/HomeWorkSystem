import React, { useState } from "react";
import { Button, Row, Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLock, faLockOpen, faTasks } from "@fortawesome/free-solid-svg-icons";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store/rootReducer";
import { assignmentService } from "../services/assignmentService";
import { freeAssignment, reserveAssignment } from "../store/assignmentStore";
import GradeAssignment from "./modals/GradeAssignment";
import { IAssignmentResponse } from "../models/assignment";

const FilteredAssignments: React.FC = () => {
  const dispatch = useDispatch();
  const user = useSelector((state: RootState) => state.userReducer.user);
  const filteredAssignments = useSelector(
    (state: RootState) => state.assignmentReducer.filteredAssignments
  );
  const [assignmentToGrade, setAssignmentToGrade] = useState<
    IAssignmentResponse
  >({} as IAssignmentResponse);
  const [showGradeAssignment, setShowGradeAssignment] = useState<boolean>(
    false
  );

  const onGradeClick = (assignment: IAssignmentResponse) => () => {
    setAssignmentToGrade(assignment);
    setShowGradeAssignment(true);
  };

  const onFreeClick = (assignmentId: string) => async () => {
    const success = await assignmentService.free(assignmentId);
    if (success) {
      dispatch(freeAssignment(assignmentId));
    }
  };

  const onReserveClick = (assignmentId: string) => async () => {
    const success = await assignmentService.reserve(assignmentId);
    if (success && user !== undefined) {
      dispatch(
        reserveAssignment({ assignmentId: assignmentId, userId: user.id })
      );
    }
  };

  return (
    <div>
      <Table bordered>
        <thead>
          <tr>
            <th style={colStyle.group}>Csoport</th>
            <th style={colStyle.hw}>Házi feladat</th>
            <th style={colStyle.student}>Hallgató</th>
            <th style={colStyle.grade}>Eredmény</th>
            <th style={colStyle.buttons}></th>
          </tr>
        </thead>
        <tbody>
          {filteredAssignments.map((a) => (
            <tr key={a.id}>
              <td style={colStyle.group}>{a.groupName}</td>
              <td style={colStyle.hw}>{a.homeworkTitle}</td>
              <td style={colStyle.student}>
                {a.userFullName} ({a.userName})
              </td>
              <td style={colStyle.grade}>{a.grade}</td>
              <td style={colStyle.buttons}>
                {a.reservedBy === user?.id ? (
                  <Row>
                    <Button
                      size="sm"
                      className="ml-auto mr-2"
                      onClick={onGradeClick(a)}
                    >
                      <FontAwesomeIcon icon={faTasks} className="mr-2" />
                      Értékel
                    </Button>
                    <Button
                      size="sm"
                      className="mr-2"
                      onClick={onFreeClick(a.id)}
                    >
                      <FontAwesomeIcon icon={faLockOpen} className="mr-2" />
                      Felszabadít
                    </Button>
                  </Row>
                ) : (
                  <Row>
                    <Button
                      size="sm"
                      className="ml-auto mr-2"
                      onClick={onReserveClick(a.id)}
                    >
                      <FontAwesomeIcon icon={faLock} className="mr-2" />
                      Lefoglal
                    </Button>
                  </Row>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      {/* Modals */}
      {showGradeAssignment && (
        <GradeAssignment
          showGradeAssignment={showGradeAssignment}
          setShowGradeAssignment={setShowGradeAssignment}
          assignment={assignmentToGrade}
        />
      )}
    </div>
  );
};

export default FilteredAssignments;

const colStyle = {
  group: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  hw: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  student: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  grade: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  buttons: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
};
