import React, { useState } from "react";
import { Button, Row, Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCalendarPlus,
  faComments,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { IGroupResponse } from "../models/group";
import { IHomeworkResponse } from "../models/homework";
import { CommentScope } from "../shared/enums";
import HomeworkDetails from "./modals/HomeworkDetails";
import Comments from "./modals/Comments";
import NewHomework from "./modals/NewHomework";

interface TeacherGroupProps {
  group: IGroupResponse;
}

const TeacherGroup: React.FC<TeacherGroupProps> = ({ group }) => {
  const [showNewHomework, setShowNewHomework] = useState<boolean>(false);
  const [showHomeworkDetails, setShowHomeworkDetails] = useState<boolean>(
    false
  );
  const [homeworkToShow, setHomeworkToShow] = useState<IHomeworkResponse>(
    {} as IHomeworkResponse
  );
  const [showComments, setShowComments] = useState<boolean>(false);
  const [scope, setScope] = useState<CommentScope>(CommentScope.GROUP);
  const [scopeId, setScopeId] = useState<string>("");

  const onNewHomeworkClick = () => {
    setShowNewHomework(true);
  };

  const onDetailsClick = (homeworkId: string) => () => {
    setShowHomeworkDetails(true);
    const homeworkToShow = group.homeworks.find((h) => h.id === homeworkId);
    if (homeworkToShow !== undefined) {
      setHomeworkToShow(homeworkToShow);
    }
  };

  const onCommentsClick = (scope: CommentScope, id: string) => () => {
    setShowComments(true);
    setScope(scope);
    setScopeId(id);
  };

  return (
    <div className="mt-3">
      <Table bordered>
        <thead>
          <tr>
            <th style={colStyle.name}>
              {group.name}
              <Button
                variant="info"
                size="sm"
                className="ml-2"
                onClick={onCommentsClick(CommentScope.GROUP, group.id)}
              >
                <FontAwesomeIcon icon={faComments} className="mr-2" />
                közlemények
              </Button>
            </th>
            <th style={colStyle.code}>Csoport kódja: {group.code}</th>
            <th style={colStyle.newHw}>
              <Row>
                <Button
                  size="sm"
                  className="ml-auto mr-2"
                  onClick={onNewHomeworkClick}
                >
                  <FontAwesomeIcon icon={faCalendarPlus} className="mr-2" />
                  Új házi feladat
                </Button>
              </Row>
            </th>
          </tr>
        </thead>
        <tbody>
          {group.homeworks.map((homework) => (
            <tr key={homework.id}>
              <td colSpan={3}>
                <Row className="ml-2">
                  {homework.title}
                  <Button
                    size="sm"
                    variant="info"
                    className="ml-auto mr-2"
                    onClick={onDetailsClick(homework.id)}
                  >
                    <FontAwesomeIcon icon={faInfoCircle} className="mr-2" />
                    részletek
                  </Button>
                  <Button
                    size="sm"
                    variant="info"
                    className="mr-2"
                    onClick={onCommentsClick(
                      CommentScope.HOMEWORK,
                      homework.id
                    )}
                  >
                    <FontAwesomeIcon icon={faComments} className="mr-2" />
                    közlemények
                  </Button>
                </Row>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      {/* Modals */}
      {showNewHomework && (
        <NewHomework
          showNewHomework={showNewHomework}
          setShowNewHomework={setShowNewHomework}
          group={group}
        />
      )}
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
    </div>
  );
};

export default TeacherGroup;

const colStyle = {
  name: {
    width: "60%",
  } as React.CSSProperties,
  code: {
    width: "25%",
  } as React.CSSProperties,
  newHw: {
    width: "15%",
  } as React.CSSProperties,
};
