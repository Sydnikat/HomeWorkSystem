import React from "react";
import { Col, Container, Row } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useGroups } from "../shared/hooks";
import { RootState } from "../store/rootReducer";
import TeacherGroup from "./TeacherGroup";

const TeacherGroups: React.FC = () => {
  const user = useSelector((state: RootState) => state.userReducer.user);
  const groups = useGroups();

  return (
    <div className="mt-5">
      <Container fluid>
        <Row>
          <Col>
            <h2>Saját csoportjaim</h2>
            {groups
              .filter((group) => group.ownerId === user?.id)
              .map((group) => (
                <TeacherGroup key={group.id} group={group} />
              ))}
            <h2 className="mt-5">További csoportjaim</h2>
            {groups
              .filter((group) => group.ownerId !== user?.id)
              .map((group) => (
                <TeacherGroup key={group.id} group={group} />
              ))}
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default TeacherGroups;
