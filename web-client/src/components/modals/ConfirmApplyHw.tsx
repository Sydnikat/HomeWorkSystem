import React, { useState } from "react";
import { Button, Modal } from "react-bootstrap";
import CommonAlert from "../CommonAlert";

interface ConfirmApplyHwProps {
  showConfirmApplyHw: boolean;
  setShowConfirmApplyHw: React.Dispatch<React.SetStateAction<boolean>>;
  groupId: string;
}

const ConfirmApplyHw: React.FC<ConfirmApplyHwProps> = ({
  showConfirmApplyHw,
  setShowConfirmApplyHw,
  groupId,
}) => {
  const [applyError, setApplyError] = useState<boolean>(false);

  const onApplyClick = () => {
    console.log("apply hw - call homework  api with groupId", groupId);
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
        <div>
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
