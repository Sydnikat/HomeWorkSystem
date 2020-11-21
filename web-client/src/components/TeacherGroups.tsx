import React, {useEffect} from "react";
import { Col, Container, Row, Spinner } from "react-bootstrap";
import {useDispatch, useSelector} from "react-redux";
import { useGroups } from "../shared/hooks";
import { setGroups } from '../store/groupStore';
import { RootState } from "../store/rootReducer";
import TeacherGroup from "./TeacherGroup";

const TeacherGroups: React.FC = () => {
  const dispatch = useDispatch();
  const user = useSelector((state: RootState) => state.userReducer.user);
  const { groups: fetchedGroups, groupsLoading } = useGroups();
  const groups = useSelector((state: RootState) => state.groupReducer.groups);

  useEffect(() => {
    dispatch(setGroups(fetchedGroups));
  }, [fetchedGroups, dispatch]);

  return (
    <div className="mt-5">
      <Container fluid>
        <Row>
          <Col>
            <h2>Saját csoportjaim</h2>
            {groupsLoading ? (
              <Spinner animation="border" variant="primary" />
            ) : (
              groups
                .filter((group) => group.ownerId === user?.id)
                .map((group) => <TeacherGroup key={group.id} group={group} />)
            )}
            <h2 className="mt-5">További csoportjaim</h2>
            {groupsLoading ? (
              <Spinner animation="border" variant="primary" />
            ) : (
              groups
                .filter((group) => group.ownerId !== user?.id)
                .map((group) => <TeacherGroup key={group.id} group={group} />)
            )}
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default TeacherGroups;
