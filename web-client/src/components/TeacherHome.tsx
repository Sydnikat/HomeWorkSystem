import React, { useState } from "react";
import { Button, Col, Container, Row } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faPlus,
  faSignOutAlt,
  faUsers,
  faTasks,
} from "@fortawesome/free-solid-svg-icons";
import { useDispatch } from "react-redux";
import { setUser } from "../store/userStore";
import { IUser } from "../models/user";
import { Link, Route } from "react-router-dom";
import TeacherGroups from "./TeacherGroups";
import Grading from "./Grading";
import NewGroup from "./modals/NewGroup";
import { setGroups } from "../store/groupStore";
import {setAssignments, setFilteredAssignments} from "../store/assignmentStore";
import {setComments} from "../store/commentStore";

const TeacherHome: React.FC = () => {
  const dispatch = useDispatch();

  const [showNewGroup, setShowNewGroup] = useState<boolean>(false);

  const onNewGroupClick = () => {
    setShowNewGroup(true);
  };

  const onSignout = () => {
    dispatch(setUser({} as IUser));
    dispatch(setGroups([]));
    dispatch(setAssignments([]));
    dispatch(setFilteredAssignments([]));
    dispatch(setComments([]));
  };

  return (
    <div>
      <Container fluid className="mt-2 mb-2">
        <Row>
          <Col md={{ span: 10, offset: 1 }}>
            <Row>
              <Button size="sm" className="mr-2" onClick={onNewGroupClick}>
                <FontAwesomeIcon icon={faUsers} className="mr-1" />
                <FontAwesomeIcon icon={faPlus} size="sm" className="mr-2" />
                Új csoport
              </Button>
              <Link to="/teacher/groups">
                <Button variant="secondary" size="sm" className="mr-2">
                  <FontAwesomeIcon icon={faUsers} className="mr-2" />
                  Csoportok
                </Button>
              </Link>
              <Link to="/teacher/grading">
                <Button variant="secondary" size="sm" className="mr-2">
                  <FontAwesomeIcon icon={faTasks} className="mr-2" />
                  Értékelés
                </Button>
              </Link>
              <Button
                variant="secondary"
                size="sm"
                className="ml-auto"
                onClick={onSignout}
              >
                <FontAwesomeIcon icon={faSignOutAlt} className="mr-2" />
                Kijelentkezés
              </Button>
            </Row>
            <Route exact path="/teacher/groups" component={TeacherGroups} />
            <Route exact path="/teacher/grading" component={Grading} />
          </Col>
        </Row>
      </Container>

      {/* Modals */}
      {showNewGroup && (
        <NewGroup
          showNewGroup={showNewGroup}
          setShowNewGroup={setShowNewGroup}
        />
      )}
    </div>
  );
};

export default TeacherHome;
