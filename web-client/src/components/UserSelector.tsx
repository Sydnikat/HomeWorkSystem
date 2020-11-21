import React, {useEffect, useState} from "react";
import Dropdown from "react-bootstrap/Dropdown";
import {IUserResponse} from "../models/user";
import {InputGroup} from "react-bootstrap";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSearch} from "@fortawesome/free-solid-svg-icons";

interface Props {
  title: string;
  listOfUsers: IUserResponse[];
  onSelectUser: (user: IUserResponse) => void;
}

const UserSelector: React.FC<Props> = (props: Props) => {
  const {
    title,
    listOfUsers,
    onSelectUser
  } = props;

  const [dropdownOpen, setDropdownOpen] = useState<boolean>(false);
  const [isDropdownCaptured, setIsDropdownCaptured] = useState<boolean>(false);
  const [filterStr, setFilterStr] = useState<string>("");

  useEffect(() => {
    setDropdownOpen(isDropdownCaptured);
  }, [isDropdownCaptured]);

  const searchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFilterStr(event.target.value.toLocaleLowerCase());
  };

  const searchInputEnter = () => {
    setDropdownOpen(listOfUsers.length > 0);
  };

  const searchInputLeave = () => {
    if (!isDropdownCaptured) {
      setDropdownOpen(false);
    }
  };

  const captureDropdown = () => {
    setIsDropdownCaptured(true);
  };

  const leaveDropdown = () => {
    setIsDropdownCaptured(false);
  };

  return (
    <div>

      <div>
        <div className="d-flex justify-content-between">
          <label htmlFor="search" className="col-form-label">
            {title}
          </label>
        </div>
        <InputGroup>
          <input
            onChange={searchChange}
            onFocus={searchInputEnter}
            onBlur={searchInputLeave}
            name="search"
            type="text"
            autoComplete="off"
            className="form-control custom-form-control-input input-field-label"
          />
          <InputGroup.Append>
            <InputGroup.Text>
              <FontAwesomeIcon icon={faSearch} />
            </InputGroup.Text>
          </InputGroup.Append>
        </InputGroup>
      </div>

      <Dropdown drop="down" show={dropdownOpen}>
        <Dropdown.Toggle as={"div"} bsPrefix={"hidden"}  />
        <Dropdown.Menu
          onMouseOver={captureDropdown}
          onMouseLeave={leaveDropdown}
        >
          {listOfUsers.length > 0 && listOfUsers
            .filter(u => u.userFullName.toLocaleLowerCase().includes(filterStr) || u.userName.toLocaleLowerCase().includes(filterStr))
            .map((user: IUserResponse) => (
            <Dropdown.Item
              key={user.id}
              as="div"
            >
              <div
                aria-hidden
                role="button"
                className="d-flex"
                onClick={() => {
                  onSelectUser(user);
                }}
              >
                {`${user.userFullName} (${user.userName})`}
              </div>

            </Dropdown.Item>
          ))}
        </Dropdown.Menu>
      </Dropdown>
    </div>
  );
};

export default UserSelector;