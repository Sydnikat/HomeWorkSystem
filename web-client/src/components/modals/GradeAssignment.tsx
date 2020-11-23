import React, { useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDownload, faSave } from "@fortawesome/free-solid-svg-icons";
import { IAssignmentResponse } from "../../models/assignment";
import { assignmentService } from "../../services/assignmentService";
import { gradeAssignment } from "../../store/assignmentStore";
import { useDispatch } from "react-redux";

interface GradeAssignmentProps {
  showGradeAssignment: boolean;
  setShowGradeAssignment: React.Dispatch<React.SetStateAction<boolean>>;
  assignment: IAssignmentResponse;
}

const GradeAssignment: React.FC<GradeAssignmentProps> = ({
  showGradeAssignment,
  setShowGradeAssignment,
  assignment,
}) => {
  const dispatch = useDispatch();
  const [grade, setGrade] = useState<string>("");

  const onGradeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setGrade(event.target.value);
  };

  const onDownloadClick = async () => {
    await assignmentService.download(assignment);
  };

  const onSaveClick = async () => {
    const success = await assignmentService.grade(assignment.id, grade);
    if (success) {
      dispatch(gradeAssignment({ assignmentId: assignment.id, grade: grade }));
    }
    setShowGradeAssignment(false);
  };

  const handleClose = () => {
    setShowGradeAssignment(false);
  };

  return (
    <Modal animation={false} show={showGradeAssignment} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Értékelés</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h4>{assignment.groupName}</h4>
        <h5>{assignment.homeworkTitle}</h5>
        <div>
          {assignment.userFullName} ({assignment.userName})
        </div>
        <div>Beadva: {assignment.turnInDate}</div>
        <Button size="sm" onClick={onDownloadClick}>
          <FontAwesomeIcon icon={faDownload} className="mr-2" />
          Fájl letöltése
        </Button>
        <Form className="mt-2">
          <Form.Group controlId="grade">
            <Form.Label>Értékelés</Form.Label>
            <Form.Control
              type="text"
              as="textarea"
              value={grade}
              onChange={onGradeChange}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button size="sm" onClick={onSaveClick}>
          <FontAwesomeIcon icon={faSave} className="mr-2" />
          Mentés
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default GradeAssignment;
