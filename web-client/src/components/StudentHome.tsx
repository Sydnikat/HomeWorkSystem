import React, { useState } from "react";
import { Button, Col, Container, Row, Spinner } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faSignOutAlt,
  faUsers,
  faPlus,
} from "@fortawesome/free-solid-svg-icons";
import { useDispatch } from "react-redux";
import { setUser } from "../store/userStore";
import { IUser } from "../models/user";
import StudentGroup from "./StudentGroup";
import { IGroupResponse } from "../models/group";
import JoinGroup from "./modals/JoinGroup";
import { useAssignments, useGroups } from "../shared/hooks";

const StudentHome: React.FC = () => {
  const dispatch = useDispatch();
  const { groups, groupsLoading } = useGroups();
  const { assignments, assignmentsLoading } = useAssignments();
  const [showJoinGroup, setShowJoinGroup] = useState<boolean>(false);

  const onSignout = () => {
    dispatch(setUser({} as IUser));
  };

  const onJoinClick = () => {
    setShowJoinGroup(true);
  };

  return (
    <div>
      <Container fluid className="mt-2 mb-2">
        <Row>
          <Col md={{ span: 10, offset: 1 }}>
            <Row>
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
            <Row className="mt-2">
              <Button size="sm" className="ml-auto" onClick={onJoinClick}>
                <FontAwesomeIcon icon={faUsers} className="mr-1" />
                <FontAwesomeIcon icon={faPlus} size="sm" className="mr-2" />
                Csatlakozás csoporthoz
              </Button>
            </Row>
            {groupsLoading || assignmentsLoading ? (
              <Spinner animation="border" variant="primary" />
            ) : (
              groups.map((group: IGroupResponse) => (
                <StudentGroup
                  key={group.id}
                  group={group}
                  assignments={assignments.filter(
                    (a) => a.groupId === group.id
                  )}
                />
              ))
            )}
          </Col>
        </Row>
      </Container>

      {/* Modals */}
      {showJoinGroup && (
        <JoinGroup
          showJoinGroup={showJoinGroup}
          setShowJoinGroup={setShowJoinGroup}
        />
      )}
    </div>
  );
};

export default StudentHome;
