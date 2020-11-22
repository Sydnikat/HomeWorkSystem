import React, { useState } from "react";
import { Button, Col, Form, Modal, Row, Spinner } from "react-bootstrap/";
import { useDispatch } from "react-redux";
import { groupService } from "../../services/groupService";
import { addNewGroup } from "../../store/groupStore";
import CommonAlert from "../CommonAlert";

interface JoinGroupProps {
  showJoinGroup: boolean;
  setShowJoinGroup: React.Dispatch<React.SetStateAction<boolean>>;
}

const JoinGroup: React.FC<JoinGroupProps> = ({
  showJoinGroup,
  setShowJoinGroup,
}) => {
  const dispatch = useDispatch();
  const [code, setCode] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  const [joinError, setJoinError] = useState<boolean>(false);

  const onFormControlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCode(event.target.value);
  };

  const onJoinClick = async () => {
    setJoinError(false);
    setLoading(true);
    const joinedGroup = await groupService.joinGroup(code);
    setLoading(false);
    if (joinedGroup !== null) {
      dispatch(addNewGroup(joinedGroup));
      setShowJoinGroup(false);
    } else {
      setJoinError(true);
    }
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
        <Row>
          {loading && (
            <div className="mr-2">
              <Spinner animation="border" variant="primary" />
            </div>
          )}
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
