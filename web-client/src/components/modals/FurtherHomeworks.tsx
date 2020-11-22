import React from "react";
import { Button, Modal } from "react-bootstrap";
import { IHomeworkResponse } from "../../models/homework";

interface FurtherHomeworksProps {
  showFurtherHomeworks: boolean;
  setShowFurtherHomeworks: React.Dispatch<React.SetStateAction<boolean>>;
  furtherHomeworks: IHomeworkResponse[];
  setShowConfirmApplyHw: React.Dispatch<React.SetStateAction<boolean>>;
  setHomeworkToApply: React.Dispatch<React.SetStateAction<IHomeworkResponse | null>>;
}

const FurtherHomeworks: React.FC<FurtherHomeworksProps> = ({
  showFurtherHomeworks,
  setShowFurtherHomeworks,
  furtherHomeworks,
  setShowConfirmApplyHw,
  setHomeworkToApply,
}) => {
  const onApplyClick = (homework: IHomeworkResponse) => () => {
    setHomeworkToApply(homework);
    setShowFurtherHomeworks(false);
    setShowConfirmApplyHw(true);
  };

  const handleClose = () => {
    setShowFurtherHomeworks(false);
  };

  return (
    <div>
      <Modal animation={false} show={showFurtherHomeworks} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>További házi feladatok</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ maxHeight: "50vh", overflowY: "auto" }}>
          {furtherHomeworks.length !== 0 ? (
            furtherHomeworks.map((f: IHomeworkResponse) => (
              <div key={f.id} className="mb-4">
                <div>
                  <h4>{f.title}</h4>
                </div>
                <div>
                  <b>Jelentkezési határidő:</b>{" "}
                  {f.applicationDeadline || "nincs"}
                </div>
                <div>
                  <b>Beadási határidő:</b> {f.submissionDeadline}
                </div>
                <div>
                  <b>Létszám:</b> {f.currentNumberOfStudents}
                </div>
                <div>
                  <b>Maximális fájlméret:</b> {f.maxFileSize} MB
                </div>
                <p className="mt-2">{f.description}</p>
                <div>
                  <Button size="sm" onClick={onApplyClick(f)}>
                    Jelentkezés
                  </Button>
                </div>
              </div>
            ))
          ) : (
            <div>Nincsenek további házi feladatok</div>
          )}
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default FurtherHomeworks;
