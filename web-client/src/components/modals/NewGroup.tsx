import React from "react";
import { Button, Modal } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSave } from "@fortawesome/free-solid-svg-icons";
import { groupService } from "../../services/groupService";
import { IGroupRequest } from "../../models/group";

interface NewGroupProps {
  showNewGroup: boolean;
  setShowNewGroup: React.Dispatch<React.SetStateAction<boolean>>;
}

const NewGroup: React.FC<NewGroupProps> = ({
  showNewGroup,
  setShowNewGroup,
}) => {
  const onSaveClick = async () => {
    const group = {} as IGroupRequest;
    await groupService.createGroup(group);
  };

  const handleClose = () => {
    setShowNewGroup(false);
  };

  return (
    <Modal animation={false} show={showNewGroup} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Új csoport</Modal.Title>
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

export default NewGroup;
