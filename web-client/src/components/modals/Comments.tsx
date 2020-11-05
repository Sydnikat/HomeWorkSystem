import React, { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";
import { Button, Form, Modal, Row } from "react-bootstrap/";
import { ICommentResponse } from "../../models/comment";
import { groupService } from "../../services/groupService";
import { homeworkService } from "../../services/homeworkService";
import { commentScope } from "../../shared/enums";

interface CommentsProps {
  showComments: boolean;
  setShowComments: React.Dispatch<React.SetStateAction<boolean>>;
  scope: commentScope;
  scopeId: string;
}

const Comments: React.FC<CommentsProps> = ({
  showComments,
  setShowComments,
  scope,
  scopeId,
}) => {
  const [comments, setComments] = useState<ICommentResponse[]>([]);

  useEffect(() => {
    const fetchComments = () => {
      switch (scope) {
        case commentScope.GROUP: {
          const fetchedComments = groupService.getComments(scopeId);
          setComments(fetchedComments);
          break;
        }
        case commentScope.HOMEWORK: {
          const fetchedComments = homeworkService.getComments(scopeId);
          setComments(fetchedComments);
          break;
        }
        default:
          break;
      }
    };

    fetchComments();
  }, []);

  const [newComment, setNewComment] = useState<string>("");
  const onFormControlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setNewComment(event.target.value);
  };

  const handleClose = () => {
    setShowComments(false);
  };

  return (
    <div>
      <Modal animation={false} show={showComments} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Közlemények</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ maxHeight: "50vh", overflowY: "auto" }}>
          {comments.map((comment: ICommentResponse) => (
            <div key={comment.id}>
              <div>
                <b>{comment.createdBy}</b>
                <i className="ml-2">{comment.creationDate}</i>
              </div>
              <p>{comment.content}</p>
            </div>
          ))}
        </Modal.Body>
        <Modal.Footer>
          <Row className="w-100">
            <Form className="w-100">
              <Form.Group controlId="comment">
                <Form.Control
                  type="text"
                  as="textarea"
                  placeholder="Közölj valamit"
                  value={newComment}
                  onChange={onFormControlChange}
                />
              </Form.Group>
            </Form>
          </Row>
          <Row>
            <Button size="sm">
              <FontAwesomeIcon icon={faPaperPlane} className="mr-2" />
              Küldés
            </Button>
          </Row>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default Comments;
