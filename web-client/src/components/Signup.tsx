import React, { useState } from "react";
import {
  Button,
  Col,
  Container,
  Dropdown,
  DropdownButton,
  Form,
  Row,
  Spinner,
} from "react-bootstrap";
import { useHistory } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowCircleLeft } from "@fortawesome/free-solid-svg-icons";
import { authService } from "../services/authService";
import { InputType } from "../shared/enums";
import CommonAlert from "./CommonAlert";

const roles = {
  student: {
    value: "Student",
    name: "Hallgató",
  },
  teacher: {
    value: "Teacher",
    name: "Oktató",
  },
};

const Signup: React.FC = () => {
  const history = useHistory();

  const [userName, setUserName] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [userFullName, setUserFullName] = useState<string>("");
  const [role, setRole] = useState<{ value: string; name: string }>(
    roles.student
  );
  const [inTransaction, setInTransaction] = useState<boolean>(false);
  const [signupError, setSignupError] = useState<boolean>(false);

  const onFormControlChange = (type: InputType) => (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    switch (type) {
      case InputType.USERNAME:
        setUserName(event.target.value);
        break;
      case InputType.PASSWORD:
        setPassword(event.target.value);
        break;
      case InputType.CONFIRMPASSWORD:
        setConfirmPassword(event.target.value);
        break;
      case InputType.USERFULLNAME:
        setUserFullName(event.target.value);
        break;
      default:
        break;
    }
  };

  const onRoleSelect = (selectedRole: {
    name: string;
    value: string;
  }) => () => {
    setRole(selectedRole);
  };

  const onBackClick = () => {
    history.goBack();
  };

  const onSignupClick = async () => {
    setInTransaction(true);
    const success = await authService.signup(
      userName,
      password,
      confirmPassword,
      userFullName,
      role.value
    );
    setInTransaction(false);

    if (!success) {
      setSignupError(true);
    } else {
      history.push("signin");
    }
  };

  return (
    <Container fluid>
      <Row>
        <Col md={{ span: 4, offset: 4 }}>
          <h1>Regisztráció</h1>
        </Col>
      </Row>
      <Row className="mt-2">
        <Col md={{ span: 4, offset: 4 }}>
          <Row>
            <Col>
              <Form>
                <Form.Group controlId="username">
                  <Form.Label>Felhasználónév</Form.Label>
                  <Form.Control
                    size="sm"
                    type="text"
                    placeholder="Pl.: kovacs88"
                    value={userName}
                    onChange={onFormControlChange(InputType.USERNAME)}
                  />
                </Form.Group>
                <Form.Group controlId="password">
                  <Form.Label>Jelszó</Form.Label>
                  <Form.Control
                    size="sm"
                    type="password"
                    placeholder="Jelszó"
                    value={password}
                    onChange={onFormControlChange(InputType.PASSWORD)}
                  />
                </Form.Group>
                <Form.Group controlId="confirmPassword">
                  <Form.Label>Jelszó még egyszer</Form.Label>
                  <Form.Control
                    size="sm"
                    type="password"
                    placeholder="Jelszó még egyszer"
                    value={confirmPassword}
                    onChange={onFormControlChange(InputType.CONFIRMPASSWORD)}
                  />
                </Form.Group>
                <Form.Group controlId="userfullname">
                  <Form.Label>Teljes név</Form.Label>
                  <Form.Control
                    size="sm"
                    type="text"
                    placeholder="Pl.: Dr. Kovács Tamás"
                    value={userFullName}
                    onChange={onFormControlChange(InputType.USERFULLNAME)}
                  />
                </Form.Group>
                <DropdownButton
                  variant="info"
                  size="sm"
                  id="role"
                  title={role.name}
                >
                  <Dropdown.Item onSelect={onRoleSelect(roles.student)}>
                    {roles.student.name}
                  </Dropdown.Item>
                  <Dropdown.Item onSelect={onRoleSelect(roles.teacher)}>
                    {roles.teacher.name}
                  </Dropdown.Item>
                </DropdownButton>
                <Row className="mt-2">
                  <Col>
                    <Button
                      size="sm"
                      className="mt-2"
                      onClick={onSignupClick}
                      disabled={inTransaction}
                    >
                      Regisztráció
                    </Button>
                    {inTransaction && (
                      <Spinner
                        as="span"
                        animation="border"
                        variant="primary"
                        size="sm"
                        className="ml-2"
                      />
                    )}
                  </Col>
                </Row>
                <Row className="mt-2">
                  <Col>
                    <Button variant="secondary" size="sm" onClick={onBackClick}>
                      <FontAwesomeIcon
                        icon={faArrowCircleLeft}
                        className="mr-2"
                      />
                      Vissza
                    </Button>
                  </Col>
                </Row>
              </Form>
            </Col>
          </Row>
          {signupError && (
            <Row className="mt-2">
              <Col>
                <CommonAlert
                  variant="danger"
                  text="Hiba a regisztráció közben"
                />
              </Col>
            </Row>
          )}
        </Col>
      </Row>
    </Container>
  );
};

export default Signup;
