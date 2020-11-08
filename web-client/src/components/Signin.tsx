import React, { useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { authService } from "../services/authService";
import { setUser } from "../store/userStore";
import CommonAlert from "./CommonAlert";

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
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    switch (type) {
      case inputType.USERNAME:
        setUserName(event.target.value);
        break;
      case inputType.PASSWORD:
        setPassword(event.target.value);
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
          <Row>
            <Col>
              <h1>Bejelentkezés</h1>
            </Col>
          </Row>
          <Row>
            <Col>
              <Form>
                <Form.Group controlId="username">
                  <Form.Label>Felhasználónév</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Pl.: kovacs88"
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
            <Row className="mt-2">
              <Col>
                <CommonAlert
                  variant="danger"
                  text="Hiba a bejelentkezés közben"
                />
              </Col>
            </Row>
          )}
          <br />
          <Row>
            <Col>Nincs még felhasználói fiókja?</Col>
          </Row>
          <Row>
            <Col>
              <Button onClick={onSignup}>Regisztráció</Button>
            </Col>
          </Row>
        </Col>
      </Row>
    </Container>
  );
};

export default Signin;
