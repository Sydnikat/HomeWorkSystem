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
        <p className="mt-2">{homework.description}</p>
      </Modal.Body>
    </Modal>
  );
};

export default HomeworkDetails;
