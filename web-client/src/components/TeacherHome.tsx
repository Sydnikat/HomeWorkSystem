import React from "react";
import { Button, Container, Row } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";
import { useDispatch } from "react-redux";
import { setUser } from "../store/userStore";
import { IUser } from "../models/user";

const TeacherHome: React.FC = () => {
  const dispatch = useDispatch();

  const onSignout = () => {
    dispatch(setUser({} as IUser));
  };

  return (
    <Container>
      <Row>
        <Button onClick={onSignout}>
          <FontAwesomeIcon icon={faSignOutAlt} />
          Kijelentkezés
        </Button>
      </Row>
      <Row>
        <div>TeacherHome</div>
      </Row>
    </Container>
  );
};

export default TeacherHome;
