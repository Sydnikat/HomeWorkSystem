import React, {useEffect, useState} from "react";
import {
  Button,
  Container,
  Form,
  Modal
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSave } from "@fortawesome/free-solid-svg-icons";
import { groupService } from "../../services/groupService";
import { IGroupRequest } from "../../models/group";
import { IUserResponse } from "../../models/user";
import {useUsers} from "../../shared/hooks";
import UserListAssemble from "../UserListAssemble";
import {useDispatch} from "react-redux";
import {addNewGroup} from "../../store/groupStore";
import CommonAlert from "../CommonAlert";

interface NewGroupProps {
  showNewGroup: boolean;
  setShowNewGroup: React.Dispatch<React.SetStateAction<boolean>>;
}

const NewGroup: React.FC<NewGroupProps> = ({
  showNewGroup,
  setShowNewGroup,
}) => {
  const dispatch = useDispatch();
  const { users, usersLoading } = useUsers();
  const [students, setStudents] = useState<IUserResponse[]>([]);
  const [teachers, setTeachers] = useState<IUserResponse[]>([]);
  const [selectedStudents, setSelectedStudents] = useState<IUserResponse[]>([]);
  const [selectedTeachers, setSelectedTeachers] = useState<IUserResponse[]>([]);
  const [groupName, setGroupName] = useState<string>("");
  const [inTransaction, setInTransaction] = useState<boolean>(false);
  const [applyError, setApplyError] = useState<boolean>(false);

  useEffect(() => {
    if (!usersLoading) {
      setStudents(users.filter((u => u.role == "Student")));
      setTeachers(users.filter((u => u.role == "Teacher")));
    }
  }, [usersLoading]);

  const onGroupNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setGroupName(event.target.value);
  };

  const onSaveClick = async () => {
    const group: IGroupRequest = {
      name: groupName,
      students: selectedStudents.map(s => s.id),
      teachers: selectedTeachers.map(t => t.id)
    };

    setApplyError(false);
    setInTransaction(true);

    const newGroup = await groupService.createGroup(group);

    setInTransaction(false);

    if (newGroup !== null) {
      dispatch(addNewGroup(newGroup));
      setShowNewGroup(false);
    } else {
      setApplyError(true);
    }
  };

  const handleClose = () => {
    setShowNewGroup(false);
  };

  return (
    <Modal size="lg" animation={false} show={showNewGroup} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Új csoport</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Container fluid>
          <Form>
            <Form.Group controlId="newGroup">

              <Form.Label>Csoport Neve</Form.Label>
              <Form.Control
                type="text"
                placeholder="csoport Neve..."
                value={groupName}
                onChange={onGroupNameChange}
              />
            </Form.Group>
          </Form>

          <UserListAssemble
            listTitle={"Tanulók listájának megadása"}
            selectedListTitle={"Kiválasztott hallgatók"}
            notSelectedListTitle={"Nem kiválasztott hallgatók"}
            userList={students}
            selectedUsers={selectedStudents}
            setSelectedUsers={setSelectedStudents}
          />

          <UserListAssemble
            listTitle={"Tanárok listájának megadása"}
            selectedListTitle={"Kiválasztott tanárok"}
            notSelectedListTitle={"Nem kiválasztott tanárok"}
            userList={teachers}
            selectedUsers={selectedTeachers}
            setSelectedUsers={setSelectedTeachers}
          />

          {applyError && (
            <div className="mt-3">
              <CommonAlert variant="danger" text="Hiba a mentés közben" />
            </div>
          )}

        </Container>
      </Modal.Body>
      <Modal.Footer>
        <Button size="sm" disabled={inTransaction} onClick={onSaveClick}>
          <FontAwesomeIcon icon={faSave} className="mr-2" />
          Mentés
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default NewGroup;
