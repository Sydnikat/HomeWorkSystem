import React, { useState } from "react";
import { Button, Modal } from "react-bootstrap";
import CommonAlert from "../CommonAlert";
import {useDispatch} from "react-redux";
import {setAssignment} from "../../store/assignmentStore";
import {homeworkService} from "../../services/homeworkService";

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
  const [applyError, setApplyError] = useState<boolean>(false);

  const onApplyClick = async () => {
    if (homeworkId === "")
      return;

    setApplyError(false);
    const assignment = await homeworkService.createAssignment(homeworkId);

    if (assignment !== null) {
      dispatch(setAssignment(assignment));
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
        <Button onClick={onApplyClick}>Jelentkezem</Button>
      </Modal.Footer>
    </Modal>
  );
};

export default ConfirmApplyHw;
