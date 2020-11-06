import { useEffect, useState } from "react";
import { IAssignmentResponse } from "../models/assignment";
import { ICommentResponse } from "../models/comment";
import { IGroupResponse } from "../models/group";
import { assignmentService } from "../services/assignmentService";
import { groupService } from "../services/groupService";
import { homeworkService } from "../services/homeworkService";
import { CommentScope } from "./enums";

export const useGroups = (): IGroupResponse[] => {
  const [groups, setGroups] = useState<IGroupResponse[]>([]);

  useEffect(() => {
    let isSubscribed = true;

    const fetchGroups = async () => {
      try {
        const fetchedGroups = await groupService.getGroups();
        if (isSubscribed) {
          setGroups(fetchedGroups);
        }
      } catch (e) {
        console.log(e);
      }
    };

    void fetchGroups();

    return () => {
      isSubscribed = false;
    };
  }, []);

  return groups;
};

export const useAssignments = (): IAssignmentResponse[] => {
  const [assignments, setAssignments] = useState<IAssignmentResponse[]>([]);

  useEffect(() => {
    let isSubscribed = true;

    const fetchAssignments = async () => {
      try {
        const fetchedAssignments = await assignmentService.getAssignments();
        if (isSubscribed) {
          setAssignments(fetchedAssignments);
        }
      } catch (e) {
        console.log(e);
      }
    };

    void fetchAssignments();

    return () => {
      isSubscribed = false;
    };
  }, []);

  return assignments;
};

export const useComments = (
  scope: CommentScope,
  scopeId: string
): ICommentResponse[] => {
  const [comments, setComments] = useState<ICommentResponse[]>([]);

  useEffect(() => {
    let isSubscribed = true;

    const fetchComments = async () => {
      try {
        switch (scope) {
          case CommentScope.GROUP: {
            const fetchedComments = await groupService.getComments(scopeId);
            if (isSubscribed) {
              setComments(fetchedComments);
            }
            break;
          }

          case CommentScope.HOMEWORK: {
            const fetchedComments = await homeworkService.getComments(scopeId);
            if (isSubscribed) {
              setComments(fetchedComments);
            }
            break;
          }
          default:
            break;
        }
      } catch (e) {
        console.log(e);
      }
    };

    void fetchComments();

    return () => {
      isSubscribed = false;
    };
  }, []);

  return comments;
};
