import React, { useState } from "react";
import {
  Alert,
  Button,
  Col,
  Container,
  Dropdown,
  DropdownButton,
  Form,
  Row,
} from "react-bootstrap";
import { useHistory } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowCircleLeft } from "@fortawesome/free-solid-svg-icons";
import { authService } from "../service/authService";

enum inputType {
  USERNAME,
  PASSWORD,
  REPASSWORD,
  USERFULLNAME,
}

const roles = {
  student: {
    value: "student",
    name: "Hallgató",
  },
  teacher: {
    value: "teacher",
    name: "Oktató",
  },
};

const Signup: React.FC = () => {
  const history = useHistory();

  const [userName, setUserName] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [repassword, setRepassword] = useState<string>("");
  const [userFullName, setUserFullName] = useState<string>("");
  const [role, setRole] = useState<{ value: string; name: string }>(
    roles.student
  );
  const [signupError, setSignupError] = useState<boolean>(false);

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
      case inputType.REPASSWORD:
        // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
        setRepassword(event.currentTarget.value as string);
        break;
      case inputType.USERFULLNAME:
        // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
        setUserFullName(event.currentTarget.value as string);
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

  const onBack = () => {
    history.goBack();
  };

  const onSignup = async () => {
    const success = await authService.signup(
      userName,
      password,
      repassword,
      userFullName,
      role.value
    );

    if (!success) {
      setSignupError(true);
    } else {
      history.push("signin");
    }
  };

  return (
    <Container fluid>
      <Row>
        <Col md={4}>
          <Button onClick={onBack}>
            <FontAwesomeIcon icon={faArrowCircleLeft} />
          </Button>
        </Col>
        <Col md={4}>
          <h1>Regisztráció</h1>
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
            <Form.Group controlId="repassword">
              <Form.Label>Jelszó még egyszer</Form.Label>
              <Form.Control
                type="password"
                placeholder="Jelszó még egyszer"
                value={repassword}
                onChange={onFormControlChange(inputType.REPASSWORD)}
              />
            </Form.Group>
            <Form.Group controlId="userfullname">
              <Form.Label>Teljes név</Form.Label>
              <Form.Control
                type="text"
                placeholder="Dr. Kovács Tamás"
                value={userFullName}
                onChange={onFormControlChange(inputType.USERFULLNAME)}
              />
            </Form.Group>
            <DropdownButton id="role" title={role.name}>
              <Dropdown.Item onSelect={onRoleSelect(roles.student)}>
                Hallgató
              </Dropdown.Item>
              <Dropdown.Item onSelect={onRoleSelect(roles.teacher)}>
                Oktató
              </Dropdown.Item>
            </DropdownButton>
            <Button onClick={onSignup}>Regisztráció</Button>
          </Form>
        </Col>
      </Row>
      {signupError && (
        <Row>
          <Col md={{ span: 4, offset: 4 }}>
            <Alert variant="danger">Hiba a regisztráció közben</Alert>
          </Col>
        </Row>
      )}
    </Container>
  );
};

export default Signup;
