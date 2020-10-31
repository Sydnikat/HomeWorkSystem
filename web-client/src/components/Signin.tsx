import React, { useState } from "react";
import { Alert, Button, Col, Container, Form, Row } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { authService } from "../service/authService";
import { setUser } from "../store/userStore";

enum inputType {
  USERNAME,
  PASSWORD,
}

const Signin: React.FC = () => {
  const dispatch = useDispatch();
  const history = useHistory();

  const [userName, setUserName] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [signinError, setSigninError] = useState<boolean>(false);

  const onFormControlChange = (type: inputType) => (
    event: React.BaseSyntheticEvent
  ) => {
    switch (type) {
      case inputType.USERNAME:
        // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
        setUserName(event.currentTarget.value as string);
        break;
      case inputType.PASSWORD:
        // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
        setPassword(event.currentTarget.value as string);
        break;
      default:
        break;
    }
  };

  const onSignin = async () => {
    const user = await authService.signin(userName, password);
    if (user) {
      dispatch(setUser(user));
      user.role === "student"
        ? history.push("student")
        : history.push("teacher");
    } else {
      setSigninError(true);
    }
  };

  const onSignup = () => {
    history.push("signup");
  };
  return (
    <Container fluid>
      <Row>
        <Col md={{ span: 4, offset: 4 }}>
          <h1>Bejelentkezés</h1>
        </Col>
      </Row>
      <Row>
        <Col md={{ span: 4, offset: 4 }}>
          <Form>
            <Form.Group controlId="username">
              <Form.Label>Felhasználónév</Form.Label>
              <Form.Control
                type="text"
                placeholder="kovacs88"
                value={userName}
                onChange={onFormControlChange(inputType.USERNAME)}
              />
            </Form.Group>
            <Form.Group controlId="password">
              <Form.Label>Jelszó</Form.Label>
              <Form.Control
                type="password"
                placeholder="Jelszó"
                value={password}
                onChange={onFormControlChange(inputType.PASSWORD)}
              />
            </Form.Group>
            <Button onClick={onSignin}>Bejelentkezés</Button>
          </Form>
        </Col>
      </Row>
      {signinError && (
        <Row>
          <Col md={{ span: 4, offset: 4 }}>
            <Alert variant="danger">Hiba a bejelentkezés közben</Alert>
          </Col>
        </Row>
      )}
      <br />
      <Row>
        <Col md={{ span: 4, offset: 4 }}>Nincs még felhasználói fiókja?</Col>
      </Row>
      <Row>
        <Col md={{ span: 4, offset: 4 }}>
          <Button onClick={onSignup}>Regisztráció</Button>
        </Col>
      </Row>
    </Container>
  );
};

export default Signin;
