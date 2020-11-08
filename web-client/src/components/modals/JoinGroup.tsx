import React, { useState } from "react";
import { Button, Col, Form, Modal, Row } from "react-bootstrap/";
import CommonAlert from "../CommonAlert";

interface JoinGroupProps {
  showJoinGroup: boolean;
  setShowJoinGroup: React.Dispatch<React.SetStateAction<boolean>>;
}

const JoinGroup: React.FC<JoinGroupProps> = ({
  showJoinGroup,
  setShowJoinGroup,
}) => {
  const [code, setCode] = useState<string>("");
  const [joinError, setJoinError] = useState<boolean>(false);

  const onFormControlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCode(event.target.value);
  };

  const onJoinClick = () => {
    setJoinError(true);
  };

  const handleClose = () => {
    setShowJoinGroup(false);
    setCode("");
    setJoinError(false);
  };

  return (
    <Modal animation={false} show={showJoinGroup} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Csatlakozás csoporthoz</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="code">
            <Form.Label>Kód</Form.Label>
            <Form.Control
              type="text"
              placeholder="csoport kód"
              value={code}
              onChange={onFormControlChange}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Row className="w-100">
          <Button className="ml-auto" onClick={onJoinClick}>
            Csatlakozás
          </Button>
        </Row>
        {joinError && (
          <Row className="w-100">
            <Col>
              <CommonAlert variant="danger" text="Hiba a csatlakozás közben" />
            </Col>
          </Row>
        )}
      </Modal.Footer>
    </Modal>
  );
};

export default JoinGroup;
