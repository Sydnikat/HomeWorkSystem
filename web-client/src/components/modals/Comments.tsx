import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";
import { Button, Form, Modal, Row, Spinner } from "react-bootstrap/";
import { ICommentRequest, ICommentResponse } from "../../models/comment";
import { groupService } from "../../services/groupService";
import { homeworkService } from "../../services/homeworkService";
import { CommentScope } from "../../shared/enums";
import { useComments } from "../../shared/hooks";

interface CommentsProps {
  showComments: boolean;
  setShowComments: React.Dispatch<React.SetStateAction<boolean>>;
  scope: CommentScope;
  scopeId: string;
}

const Comments: React.FC<CommentsProps> = ({
  showComments,
  setShowComments,
  scope,
  scopeId,
}) => {
  const { comments, commentsLoading } = useComments(scope, scopeId);
  const [newComment, setNewComment] = useState<string>("");

  const onFormControlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setNewComment(event.target.value);
  };

  const onSendClick = async () => {
    const comment: ICommentRequest = { content: newComment };
    switch (scope) {
      case CommentScope.GROUP: {
        await groupService.sendComment(scopeId, comment);
        break;
      }
      case CommentScope.HOMEWORK: {
        await homeworkService.sendComment(scopeId, comment);
        break;
      }
      default:
        break;
    }
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
          {commentsLoading ? (
            <Spinner animation="border" variant="primary" />
          ) : (
            comments.map((comment: ICommentResponse) => (
              <div key={comment.id}>
                <div>
                  <b>{comment.createdBy}</b>
                  <i className="ml-2">{comment.creationDate}</i>
                </div>
                <p>{comment.content}</p>
              </div>
            ))
          )}
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
            <Button size="sm" onClick={onSendClick}>
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
