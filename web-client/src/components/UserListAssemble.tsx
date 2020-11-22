import React, {useEffect, useState} from "react";
import {IUserResponse} from "../models/user";
import {Button, Form, Table} from "react-bootstrap";
import { Typeahead } from 'react-bootstrap-typeahead';
import "react-bootstrap-typeahead/css/Typeahead.css";
import UserSelector from "./UserSelector";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faTimes, faTrash} from "@fortawesome/free-solid-svg-icons";

interface UserListAssembleProps {
  listTitle: string;
  selectedListTitle: string;
  notSelectedListTitle: string;
  userList: IUserResponse[];
  selectedUsers: IUserResponse[];
  setSelectedUsers: (list: IUserResponse[]) => void;
}

const UserListAssemble: React.FC<UserListAssembleProps> = (props: UserListAssembleProps) => {
  const {
    listTitle,
    selectedListTitle,
    notSelectedListTitle,
    userList,
    selectedUsers,
    setSelectedUsers
  } = props;

  const [unSelectedUsers, setUnSelectedUsers] = useState<IUserResponse[]>(userList);
  const [preSelectedUserList, setPreSelectedUserList] = useState<IUserResponse[]>([]);

  useEffect(() => {
    setUnSelectedUsers(
      userList.filter(
        s => selectedUsers.find(selected => selected.userName === s.userName) === undefined
      )
    );
  }, [userList, selectedUsers]);

  const onSelectedUserChange = (selectedList: IUserResponse[]) => {
    setPreSelectedUserList(selectedList);
  };

  const onUserPickedChange = (user: IUserResponse) => {
    const newList: IUserResponse[] = [...selectedUsers, user];
    setSelectedUsers(newList);
    setPreSelectedUserList(preSelectedUserList.filter(u => u.id !== user.id));
  };

  const onSubmitPreselectedUsersClick = () => {
    const list: IUserResponse[] = [];
    list.push(...selectedUsers);
    preSelectedUserList.forEach(preSelected => {
      if (list.find(u => u.id === preSelected.id) === undefined) {
        list.push(preSelected)
      }
    });
    setSelectedUsers(list);
    setPreSelectedUserList([]);
  };

  const removeUserFromSelected = (user: IUserResponse) => {
    setSelectedUsers(selectedUsers.filter(u => u.id !== user.id));
  };

  return (
    <div className="my-4">
      <Form>
        <div className="my-3 d-flex justify-content-end">
          <div className="flex-fill">
            <Form.Label>{listTitle}</Form.Label>

              <Typeahead
                id="selectedStudents"
                labelKey={(option) => `${option.userFullName} (${option.userName})`}
                multiple
                options={unSelectedUsers}
                selected={preSelectedUserList}
                placeholder={"Adjon meg neveket..."}
                onChange={onSelectedUserChange}
              />

          </div>
          <div className="mx-2 mb-2 mt-auto">
            <Button size="sm" onClick={onSubmitPreselectedUsersClick}>
              Kiválasztás
            </Button>
          </div>
        </div>


        <UserSelector
          title={notSelectedListTitle}
          listOfUsers={unSelectedUsers}
          onSelectUser={onUserPickedChange}
        />

        <div className="d-flex justify-content-end">
          <div className="flex-fill">
            <Form.Label className="mt-4 ">{selectedListTitle}</Form.Label>
            <Table bordered>
              <tbody>
              {selectedUsers.length > 0 && selectedUsers.map(user =>
                <tr key={user.id}>
                  <td>
                    <div className="d-flex justify-content-end">
                      <div className="flex-fill" >
                        {`${user.userFullName} (${user.userName})`}
                      </div>
                      <div
                        className="my-auto mx-3"
                        style={{cursor: "pointer"}
                        } onClick={() => {removeUserFromSelected(user)}}
                      >
                        <FontAwesomeIcon size="1x" icon={faTimes} />
                      </div>
                    </div>
                  </td>
                </tr>
              )}
              {selectedUsers.length === 0 &&
              <tr key={`${selectedListTitle}-empty`}>
                  <td>
                      <div className="d-flex justify-content-center">
                          A lista üres...
                      </div>
                  </td>
              </tr>
              }
              </tbody>
            </Table>
          </div>
          <div
            className="mt-5 mb-auto ml-4 mr-5"
            style={{cursor: "pointer"}}
            onClick={() => {setSelectedUsers([])}}
          >
            <FontAwesomeIcon size="2x" icon={faTrash} />
          </div>
        </div>
      </Form>
    </div>
  )
};

export default UserListAssemble;