import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSave } from "@fortawesome/free-solid-svg-icons";
import { Button, Modal } from "react-bootstrap";
import { IHomeworkRequest } from "../../models/homework";
import { groupService } from "../../services/groupService";

interface NewHomeworkProps {
  showNewHomework: boolean;
  setShowNewHomework: React.Dispatch<React.SetStateAction<boolean>>;
  groupId: string;
}

const NewHomework: React.FC<NewHomeworkProps> = ({
  showNewHomework,
  setShowNewHomework,
  groupId,
}) => {
  const onSaveClick = async () => {
    const homework = {} as IHomeworkRequest;
    await groupService.createHomework(groupId, homework);
  };

  const handleClose = () => {
    setShowNewHomework(false);
  };

  return (
    <Modal animation={false} show={showNewHomework} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Új házi feladat</Modal.Title>
      </Modal.Header>
      <Modal.Body>Body</Modal.Body>
      <Modal.Footer>
        <Button size="sm" onClick={onSaveClick}>
          <FontAwesomeIcon icon={faSave} className="mr-2" />
          Mentés
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default NewHomework;
