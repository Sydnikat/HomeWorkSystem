import React, { useState } from "react";
import { Button, Modal, Spinner } from "react-bootstrap";
import CommonAlert from "../CommonAlert";
import { useDispatch } from "react-redux";
import { addAssignment } from "../../store/assignmentStore";
import { homeworkService } from "../../services/homeworkService";

interface ConfirmApplyHwProps {
  showConfirmApplyHw: boolean;
  setShowConfirmApplyHw: React.Dispatch<React.SetStateAction<boolean>>;
  homeworkId: string;
}

const ConfirmApplyHw: React.FC<ConfirmApplyHwProps> = ({
  showConfirmApplyHw,
  setShowConfirmApplyHw,
  homeworkId,
}) => {
  const dispatch = useDispatch();
  const [inTransaction, setInTransaction] = useState<boolean>(false);
  const [applyError, setApplyError] = useState<boolean>(false);

  const onApplyClick = async () => {
    if (homeworkId === "") return;

    setInTransaction(true);
    setApplyError(false);
    const assignment = await homeworkService.createAssignment(homeworkId);
    setInTransaction(false);

    if (assignment !== null) {
      dispatch(addAssignment(assignment));
      setShowConfirmApplyHw(false);
    } else {
      setApplyError(true);
    }
  };

  const handleClose = () => {
    setShowConfirmApplyHw(false);
    setApplyError(false);
  };

  return (
    <Modal animation={false} show={showConfirmApplyHw} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Biztos vagy benne?</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <p>Biztos, hogy jelentkezel a házi feladatra?</p>
      </Modal.Body>
      {applyError && (
        <div className="m-2">
          <CommonAlert variant="danger" text="Hiba a jelentkezés közben" />
        </div>
      )}
      <Modal.Footer>
        {inTransaction && (
          <div className="mr-2">
            <Spinner animation="border" variant="primary" />
          </div>
        )}
        <Button onClick={onApplyClick} disabled={inTransaction}>
          Jelentkezem
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default ConfirmApplyHw;
