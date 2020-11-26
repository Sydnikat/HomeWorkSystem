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
import { CommentScope } from "../shared/enums";
import Comments from "./modals/Comments";
import FurtherHomeworks from "./modals/FurtherHomeworks";
import HomeworkDetails from "./modals/HomeworkDetails";
import Confirm from "./modals/ConfirmApplyHw";
import { assignmentService } from "../services/assignmentService";
import {useDispatch} from "react-redux";
import {changeAssignment} from "../store/assignmentStore";
import CommonAlert from "./CommonAlert";

interface StudentGroupProps {
  group: IGroupResponse;
  assignments: IAssignmentResponse[];
}

interface AssignmentError {
  assignmentId: string;
  message: string;
}

const StudentGroup: React.FC<StudentGroupProps> = ({ group, assignments }) => {
  const dispatch = useDispatch();
  const [showHomeworkDetails, setShowHomeworkDetails] = useState<boolean>(
    false
  );
  const [homeworkToShow, setHomeworkToShow] = useState<IHomeworkResponse>(
    {} as IHomeworkResponse
  );
  const [showComments, setShowComments] = useState<boolean>(false);
  const [scope, setScope] = useState<CommentScope>(CommentScope.GROUP);
  const [scopeId, setScopeId] = useState<string>("");
  const furtherHomeworks = useMemo(() => {
    return group.homeworks.filter(
      (h) =>
        !assignments.some((a) => a.homeworkId === h.id) &&
        h.currentNumberOfStudents < h.maximumNumberOfStudents
    );
  }, [group, assignments]);
  const [showFurtherHomeworks, setShowFurtherHomeworks] = useState<boolean>(
    false
  );
  const [showConfirmApplyHw, setShowConfirmApplyHw] = useState<boolean>(false);
  const [
    homeworkToApply,
    setHomeworkToApply,
  ] = useState<IHomeworkResponse | null>(null);
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [selectedFileName, setSelectedFileName] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<AssignmentError | null>(null);

  const onDetailsClick = (homeworkId: string) => () => {
    const homeworkToShow = group.homeworks.find((h) => h.id === homeworkId);
    if (homeworkToShow !== undefined) {
      setShowHomeworkDetails(true);
      setHomeworkToShow(homeworkToShow);
    }
  };

  const onCommentsClick = (scope: CommentScope, id: string) => () => {
    setShowComments(true);
    setScope(scope);
    setScopeId(id);
  };

  const onFurtherHomeworksClick = () => {
    setShowFurtherHomeworks(true);
  };

  const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event === null || event.target === null || event.target.files === null)
      return;

    setSelectedFile(event.target.files[0]);
    setSelectedFileName(event.target.files[0].name);
  };

  const onDownloadFileClick = (assignment: IAssignmentResponse) => async () => {
    await assignmentService.download(assignment);
  };

  const onUploadClick = (assignmentId: string) => async () => {
    if (selectedFile === null) {
      return ;
    }

    setErrorMessage(null);

    const formData = new FormData();
    formData.append("formFile", selectedFile);
    formData.append("fileName", selectedFileName);

    const {assignment, errorMessage} = await assignmentService.upload(assignmentId, formData);
    if (assignment !== null) {
      dispatch(
        changeAssignment(assignment)
      );
    } else {
      setErrorMessage({assignmentId: assignmentId, message: errorMessage});
    }
  };

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
                onClick={onCommentsClick(CommentScope.GROUP, group.id)}
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
          {assignments.map((a: IAssignmentResponse) => (
            <tr key={a.id}>
              <td style={colStyle.hw}>
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
                  onClick={onCommentsClick(CommentScope.HOMEWORK, a.homeworkId)}
                >
                  <FontAwesomeIcon icon={faComments} className="mr-2" />
                  közlemények
                </Button>
              </td>
              <td style={colStyle.deadline}>{a.submissionDeadline}</td>
              <td style={colStyle.file}>
                <Button
                  variant={"link"}
                  className={"ml-0 pl-0"}
                  onClick={onDownloadFileClick(a)}
                >
                  {a.fileName}
                </Button>
                <div>
                  <input type="file" onChange={onFileChange}/>
                </div>
                <div>
                  <Button
                    size="sm"
                    className="mt-1"
                    onClick={onUploadClick(a.id)}
                  >
                    <FontAwesomeIcon icon={faUpload} className="mr-2" />
                    Feltöltés
                  </Button>
                </div>
                {errorMessage !== null && errorMessage.assignmentId === a.id &&
                <div className="m-1 mt-2">
                    <CommonAlert variant="danger" text={errorMessage?.message} />
                </div>
                }
              </td>
              <td style={colStyle.grade}>{a.grade}</td>
            </tr>
          ))}
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
          setHomeworkToApply={setHomeworkToApply}
        />
      )}
      {showConfirmApplyHw && (
        <Confirm
          showConfirmApplyHw={showConfirmApplyHw}
          setShowConfirmApplyHw={setShowConfirmApplyHw}
          homeworkId={homeworkToApply !== null ? homeworkToApply.id : ""}
        />
      )}
    </div>
  );
};

export default StudentGroup;

const colStyle = {
  hw: {
    width: "45%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  deadline: {
    width: "15%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  file: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
  grade: {
    width: "20%",
    wordBreak: "break-word",
  } as React.CSSProperties,
};
