import React, { useMemo, useState } from "react";
import { Button, Col, Row, Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUpload,
  faComments,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { IAssignmentResponse } from "../models/assignment";
import { IGroupResponse } from "../models/group";
import { IHomeworkResponse } from "../models/homework";
import { commentScope } from "../shared/enums";
import Comments from "./modals/Comments";
import FurtherHomeworks from "./modals/FurtherHomeworks";
import HomeworkDetails from "./modals/HomeworkDetails";
import Confirm from "./modals/ConfirmApplyHw";

interface StudentGroupProps {
  group: IGroupResponse;
  assignments: IAssignmentResponse[];
}

const StudentGroup: React.FC<StudentGroupProps> = ({ group, assignments }) => {
  const [showHomeworkDetails, setShowHomeworkDetails] = useState<boolean>(
    false
  );
  const [homeworkToShow, setHomeworkToShow] = useState<IHomeworkResponse>();

  const onDetailsClick = (homeworkId: string) => () => {
    setShowHomeworkDetails(true);

    const homeworkToShow = group.homeworks.find((h) => h.id === homeworkId);
    if (homeworkToShow) {
      setHomeworkToShow(homeworkToShow);
    }
  };

  const [showComments, setShowComments] = useState<boolean>(false);
  const [scope, setScope] = useState<commentScope>(commentScope.GROUP);
  const [scopeId, setScopeId] = useState<string>("");

  const onCommentsClick = (scope: commentScope, id: string) => () => {
    setShowComments(true);
    setScope(scope);
    setScopeId(id);
  };

  const furtherHomeworks = useMemo(() => {
    return group.homeworks.filter(
      (h) => !assignments.some((a) => a.homeworkId === h.id)
    );
  }, [group, assignments]);

  const [showFurtherHomeworks, setShowFurtherHomeworks] = useState<boolean>(
    false
  );

  const onFurtherHomeworksClick = () => {
    setShowFurtherHomeworks(true);
  };

  const [showConfirmApplyHw, setShowConfirmApplyHw] = useState<boolean>(false);

  return (
    <div className="mt-5">
      <Row>
        <Col>
          <Row>
            <Col md="auto">
              <h2>{group.name}</h2>
            </Col>
            <Col md="auto">
              <div className="mt-2">{group.ownerFullName}</div>
            </Col>
            <Col md="auto">
              <Button
                variant="info"
                size="sm"
                onClick={onCommentsClick(commentScope.GROUP, group.id)}
              >
                <FontAwesomeIcon icon={faComments} className="mr-2" />
                közlemények
              </Button>
            </Col>
          </Row>
        </Col>
      </Row>
      <Table bordered>
        <thead>
          <tr>
            <th style={colStyle.hw}>Házi feladat</th>
            <th style={colStyle.deadline}>Határidő</th>
            <th style={colStyle.file}>Feltöltött fájl</th>
            <th style={colStyle.grade}>Eredmény</th>
          </tr>
        </thead>
        <tbody>
          {assignments.map(
            (a: IAssignmentResponse) =>
              a.groupId === group.id && (
                <tr key={a.id} style={colStyle.hw}>
                  <td>
                    {a.homeworkTitle}
                    <Button
                      variant="info"
                      size="sm"
                      className="ml-2"
                      onClick={onDetailsClick(a.homeworkId)}
                    >
                      <FontAwesomeIcon icon={faInfoCircle} className="mr-2" />
                      részletek
                    </Button>
                    <Button
                      variant="info"
                      size="sm"
                      className="ml-2"
                      onClick={onCommentsClick(
                        commentScope.HOMEWORK,
                        a.homeworkId
                      )}
                    >
                      <FontAwesomeIcon icon={faComments} className="mr-2" />
                      közlemények
                    </Button>
                  </td>
                  <td style={colStyle.deadline}>{a.submissionDeadline}</td>
                  <td style={colStyle.file}>
                    <div>{a.fileName}</div>
                    <div>
                      <input type="file" />
                    </div>
                    <div>
                      <Button size="sm" className="mt-1">
                        <FontAwesomeIcon icon={faUpload} className="mr-2" />
                        Feltöltés
                      </Button>
                    </div>
                  </td>
                  <td style={{ ...colStyle.grade, wordBreak: "break-word" }}>
                    {a.grade}
                  </td>
                </tr>
              )
          )}
        </tbody>
      </Table>
      <Row>
        <Button
          variant="secondary"
          size="sm"
          className="ml-3"
          onClick={onFurtherHomeworksClick}
        >
          Jelentkezés további házi feladatokra
        </Button>
      </Row>

      {/* Modals */}
      {showHomeworkDetails && (
        <HomeworkDetails
          showHomeworkDetails={showHomeworkDetails}
          setShowHomeworkDetails={setShowHomeworkDetails}
          homework={homeworkToShow}
        />
      )}
      {showComments && (
        <Comments
          showComments={showComments}
          setShowComments={setShowComments}
          scope={scope}
          scopeId={scopeId}
        />
      )}
      {showFurtherHomeworks && (
        <FurtherHomeworks
          showFurtherHomeworks={showFurtherHomeworks}
          setShowFurtherHomeworks={setShowFurtherHomeworks}
          furtherHomeworks={furtherHomeworks}
          setShowConfirmApplyHw={setShowConfirmApplyHw}
        />
      )}
      {showConfirmApplyHw && (
        <Confirm
          showConfirmApplyHw={showConfirmApplyHw}
          setShowConfirmApplyHw={setShowConfirmApplyHw}
          groupId={group.id}
        />
      )}
    </div>
  );
};

export default StudentGroup;

const colStyle = {
  hw: {
    width: "50%",
  },
  deadline: {
    width: "10%",
  },
  file: {
    width: "20%",
  },
  grade: {
    width: "20%",
  },
};
