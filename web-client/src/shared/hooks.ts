import { useEffect, useState } from "react";
import { IAssignmentResponse } from "../models/assignment";
import { ICommentResponse } from "../models/comment";
import { IGroupResponse } from "../models/group";
import { assignmentService } from "../services/assignmentService";
import { groupService } from "../services/groupService";
import { homeworkService } from "../services/homeworkService";
import { CommentScope } from "./enums";

export const useGroups = (): {
  groups: IGroupResponse[];
  groupsLoading: boolean;
} => {
  const [groups, setGroups] = useState<IGroupResponse[]>([]);
  const [groupsLoading, setGroupsLoading] = useState<boolean>(true);

  useEffect(() => {
    let isSubscribed = true;

    const fetchGroups = async () => {
      try {
        const fetchedGroups = await groupService.getGroups();
        if (isSubscribed) {
          setGroups(fetchedGroups);
          setGroupsLoading(false);
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

  return { groups, groupsLoading };
};

export const useAssignments = (): {
  assignments: IAssignmentResponse[];
  assignmentsLoading: boolean;
} => {
  const [assignments, setAssignments] = useState<IAssignmentResponse[]>([]);
  const [assignmentsLoading, setAssignmentsLoading] = useState<boolean>(true);

  useEffect(() => {
    let isSubscribed = true;

    const fetchAssignments = async () => {
      try {
        const fetchedAssignments = await assignmentService.getAssignments();
        if (isSubscribed) {
          setAssignments(fetchedAssignments);
          setAssignmentsLoading(false);
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

  return { assignments, assignmentsLoading };
};

export const useComments = (
  scope: CommentScope,
  scopeId: string
): { comments: ICommentResponse[]; commentsLoading: boolean } => {
  const [comments, setComments] = useState<ICommentResponse[]>([]);
  const [commentsLoading, setCommentsLoading] = useState<boolean>(true);

  useEffect(() => {
    let isSubscribed = true;

    const fetchComments = async () => {
      try {
        switch (scope) {
          case CommentScope.GROUP: {
            const fetchedComments = await groupService.getComments(scopeId);
            if (isSubscribed) {
              setComments(fetchedComments);
              setCommentsLoading(false);
            }
            break;
          }

          case CommentScope.HOMEWORK: {
            const fetchedComments = await homeworkService.getComments(scopeId);
            if (isSubscribed) {
              setComments(fetchedComments);
              setCommentsLoading(false);
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

  return { comments, commentsLoading };
};
