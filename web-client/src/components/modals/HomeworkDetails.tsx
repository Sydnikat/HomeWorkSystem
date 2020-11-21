import React from "react";
import { Modal } from "react-bootstrap";
import { IHomeworkResponse } from "../../models/homework";

interface HomeworkDetailsProps {
  showHomeworkDetails: boolean;
  setShowHomeworkDetails: React.Dispatch<React.SetStateAction<boolean>>;
  homework: IHomeworkResponse;
}

const HomeworkDetails: React.FC<HomeworkDetailsProps> = ({
  showHomeworkDetails,
  setShowHomeworkDetails,
  homework,
}) => {
  const handleClose = () => {
    setShowHomeworkDetails(false);
  };

  return (
    <Modal animation={false} show={showHomeworkDetails} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>{homework.title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div>
          <b>Határidő:</b> {homework.submissionDeadline}
        </div>
        <div>
          <b>Jelentkezési határidő:</b> {homework.applicationDeadline ?? "Nincs"}
        </div>
        <div>
          <b>Létszám:</b> {homework.currentNumberOfStudents}
        </div>
        <div>
          <b>Maximális fájlméret:</b> {homework.maxFileSize} MB
        </div>
        <div>
          <b>Javítók:</b>{" "}
          {homework.graders.map((g: string) => (
            <span key={g}>
              {g}
              {homework.graders.indexOf(g) !== homework.graders.length - 1 &&
                ", "}
            </span>
          ))}
        </div>
        <div>
          <b>Leírás:</b>
        </div>
        <p className="my-2 ml-1">{homework.description}</p>
        <div hidden={homework.students.length === 0}>
          <b>Hallgatók:</b>{" "}
          {homework.students.map((g: string) => (
            <span key={g}>
              {g}
              {homework.students.indexOf(g) !== homework.students.length - 1 &&
              ", "}
            </span>
          ))}
        </div>
      </Modal.Body>
    </Modal>
  );
};

export default HomeworkDetails;
