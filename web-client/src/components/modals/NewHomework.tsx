import React, {useEffect, useState} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSave } from "@fortawesome/free-solid-svg-icons";
import {Button, Container, Form, Modal} from "react-bootstrap";
import { IHomeworkRequest } from "../../models/homework";
import { groupService } from "../../services/groupService";
import UserListAssemble from "../UserListAssemble";
import CommonAlert from "../CommonAlert";
import {useDispatch} from "react-redux";
import {IUserResponse} from "../../models/user";
import {IGroupResponse} from "../../models/group";
import {updateGroupHomeworks} from "../../store/groupStore";

interface NewHomeworkProps {
  showNewHomework: boolean;
  setShowNewHomework: React.Dispatch<React.SetStateAction<boolean>>;
  group: IGroupResponse;
}

const NewHomework: React.FC<NewHomeworkProps> = ({
  showNewHomework,
  setShowNewHomework,
  group,
}) => {

  const dispatch = useDispatch();
  const [students, setStudents] = useState<IUserResponse[]>(group.students);
  const [graders, setGraders] = useState<IUserResponse[]>(group.teachers);
  const [selectedStudents, setSelectedStudents] = useState<IUserResponse[]>([]);
  const [selectedGraders, setSelectedGraders] = useState<IUserResponse[]>([]);
  const [homeworkTitle, setHomeworkTitle] = useState<string>("");
  const [homeworkDescription, setHomeworkDescription] = useState<string>("");
  const [homeworkFileSize, setHomeworkFileSize] = useState<number | null>(null);
  const [homeworkSubmissionDate, setHomeworkSubmission] = useState<string>("");
  const [homeworkApplicationDate, setHomeworkApplicationDate] = useState<string>("");
  const [customStudentSize, setCustomStudentSize] = useState<number | null>(null);
  const [isCustom, setIsCustom] = useState<boolean>(true);
  const [inTransaction, setInTransaction] = useState<boolean>(false);
  const [applyError, setApplyError] = useState<boolean>(false);

  useEffect(() => {
    setStudents(group.students);
    setGraders(group.teachers);
  }, [group]);

  useEffect(() => {
    if (isCustom) {
      setSelectedStudents(group.students);
    } else {
      setSelectedStudents([]);
    }
  },[isCustom]);

  const onHomeworkNameChange = (event: React.ChangeEvent<HTMLInputElement>) =>
    setHomeworkTitle(event.target.value);

  const onHomeworkDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>) =>
    setHomeworkDescription(event.target.value);

  const onHomeworkFileSizeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.value === "") {
      setHomeworkFileSize(null)
    } else {
      setHomeworkFileSize(parseInt(event.target.value, 10));
    }
  };

  const onHomeworkSubmissionDateChange = (event: React.ChangeEvent<HTMLInputElement>) =>
    setHomeworkSubmission(event.target.value);

  const onSelectCustomChange = () =>
    setIsCustom(!isCustom);

  const onHomeworkApplicationDateChange = (event: React.ChangeEvent<HTMLInputElement>) =>
    setHomeworkApplicationDate(event.target.value);

  const onHomeworkCustomStudentSizeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.value === "") {
      setCustomStudentSize(null)
    } else {
      setCustomStudentSize(parseInt(event.target.value, 10));
    }
  };

  const onSaveClick = async () => {
    let maxSize = selectedStudents.length;

    if (!isCustom) {
      if (customStudentSize !== null) {
        maxSize = customStudentSize;
      } else return;
    }

    const homework: IHomeworkRequest = {
      title: homeworkTitle,
      description: homeworkDescription,
      maxFileSize: homeworkFileSize ?? 10,
      groupId: group.id,
      submissionDeadline: homeworkSubmissionDate,
      applicationDeadline: homeworkApplicationDate !== "" ? homeworkApplicationDate : undefined,
      maximumNumberOfStudents: maxSize,
      graders: selectedGraders.map(g => g.id),
      students: selectedStudents.map(s => s.id)
    };

    setApplyError(false);
    setInTransaction(true);

    const newHomework = await groupService.createHomework(group.id, homework);

    setInTransaction(false);

    if (newHomework !== null) {
      dispatch(updateGroupHomeworks(newHomework));
      setShowNewHomework(false);
    } else {
      setApplyError(true);
    }
  };

  const formInValid = (): boolean => {
    return homeworkTitle === "" ||
      homeworkDescription === "" ||
      homeworkFileSize === null ||
      homeworkSubmissionDate === "" ||
      (!isCustom && customStudentSize === null);
  };

  const handleClose = () => {
    setShowNewHomework(false);
  };

  return (
    <Modal size="lg" animation={false} show={showNewHomework} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Új házi feladat</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Container fluid>
          <Form>
            <Form.Group controlId="title">
              <Form.Label>Házi feladat Címe</Form.Label>
              <Form.Control
                type="text"
                placeholder="Cím..."
                value={homeworkTitle}
                onChange={onHomeworkNameChange}
              />
            </Form.Group>

            <Form.Group controlId="description">
              <Form.Label>A házi feladat ismertetője</Form.Label>
              <Form.Control
                type="text"
                as="textarea"
                placeholder="Leírás..."
                value={homeworkDescription}
                onChange={onHomeworkDescriptionChange}
              />
            </Form.Group>

            <Form.Group controlId="fileSize">
              <Form.Label>A maximális fájlméret</Form.Label>
              <Form.Control
                type="number"
                min={0}
                max={50}
                placeholder="MB-ban értelmezendő..."
                value={homeworkFileSize ?? ""}
                onChange={onHomeworkFileSizeChange}
              />
            </Form.Group>

            <Form.Group controlId="submissionDate">
              <Form.Label>Beadási határidó</Form.Label>
              <Form.Control
                type="date"
                max="3000-01-01"
                value={homeworkSubmissionDate}
                onChange={onHomeworkSubmissionDateChange}
              />
            </Form.Group>

            <Form.Group controlId="applicationDate">
              <Form.Label>Feljelentkezési határidő (opcionális)</Form.Label>
              <Form.Control
                type="date"
                max="3000-01-01"
                value={homeworkApplicationDate}
                onChange={onHomeworkApplicationDateChange}
              />
            </Form.Group>

            <Form.Group controlId="selectStudents" className="my-4 d-flex justify-content-start">
              <Form.Check
                className="my-auto"
                type="switch"
                label="Ki akarom választani a hallgatókat"
                value={isCustom ? "on" : "off"}
                defaultChecked={true}
                onChange={onSelectCustomChange}
              />
            </Form.Group>

          </Form>

          {isCustom ?
            <UserListAssemble
              listTitle={"Tanulók listájának megadása"}
              selectedListTitle={"Kiválasztott hallgatók"}
              notSelectedListTitle={"Nem kiválasztott hallgatók"}
              userList={students}
              selectedUsers={selectedStudents}
              setSelectedUsers={setSelectedStudents}
            />
            :
            <Form>
              <Form.Group controlId="customStudentSize">
                <Form.Label>A maximális Létszám</Form.Label>
                <Form.Control
                  type="number"
                  min={0}
                  max={50}
                  value={customStudentSize ?? ""}
                  onChange={onHomeworkCustomStudentSizeChange}
                />
              </Form.Group>
            </Form>
          }

          <UserListAssemble
            listTitle={"Javítók listájának megadása"}
            selectedListTitle={"Kiválasztott javítók"}
            notSelectedListTitle={"Nem kiválasztott javítók"}
            userList={graders}
            selectedUsers={selectedGraders}
            setSelectedUsers={setSelectedGraders}
          />

          {applyError && (
            <div className="mt-3">
              <CommonAlert variant="danger" text="Hiba a mentés közben" />
            </div>
          )}

        </Container>
      </Modal.Body>
      <Modal.Footer>
        <Button size="sm" disabled={formInValid() || inTransaction} onClick={onSaveClick}>
          <FontAwesomeIcon icon={faSave} className="mr-2" />
          Mentés
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default NewHomework;
