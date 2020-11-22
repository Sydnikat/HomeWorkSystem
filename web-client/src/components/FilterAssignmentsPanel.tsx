import React, { useEffect, useState } from "react";
import {
  Button,
  Container,
  Dropdown,
  DropdownButton,
  Form,
  InputGroup,
  Row,
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faLock,
  faLockOpen,
  faSearch,
} from "@fortawesome/free-solid-svg-icons";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store/rootReducer";
import { setFilteredAssignments } from "../store/assignmentStore";

const FilterAssignmentPanel: React.FC = () => {
  const dispatch = useDispatch();
  const user = useSelector((state: RootState) => state.userReducer.user);
  const assignments = useSelector(
    (state: RootState) => state.assignmentReducer.assignments
  );
  const [isFreeAssignmentsSelected, setIsFreeAssignmentsSelected] = useState<
    boolean
  >(true);
  const [search, setSearch] = useState<string>("");
  const [isAllAssignmentsSelected, setIsAllAssignmentsSelected] = useState<
    boolean
  >(true);

  useEffect(() => {
    const filteredAssignments = assignments
      .filter((a) =>
        isFreeAssignmentsSelected
          ? (a.reservedBy === null || a.reservedBy === undefined )
          : a.reservedBy === user?.id
      )
      .filter(
        (a) =>
          a.groupName.toLowerCase().includes(search.toLowerCase()) ||
          a.homeworkTitle.toLowerCase().includes(search.toLowerCase()) ||
          a.userFullName.toLowerCase().includes(search.toLowerCase()) ||
          a.grade.toLowerCase().includes(search.toLowerCase())
      )
      .filter((a) => (isAllAssignmentsSelected ? true : a.grade === ""));

    dispatch(setFilteredAssignments(filteredAssignments));
  }, [
    assignments,
    dispatch,
    isAllAssignmentsSelected,
    isFreeAssignmentsSelected,
    search,
  ]);

  const onFreeAssignmentsClick = () => {
    setIsFreeAssignmentsSelected(true);
    setSearch("");
  };

  const onReservedAssignmentsClick = () => {
    setIsFreeAssignmentsSelected(false);
    setSearch("");
  };

  const onSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  };

  const onIsAllAssignmentSelectedChange = (state: boolean) => () => {
    setIsAllAssignmentsSelected(state);
  };

  return (
    <Container fluid>
      <Row>
        <Button variant="info" size="sm" onClick={onFreeAssignmentsClick}>
          <FontAwesomeIcon icon={faLockOpen} className="mr-2" />
          Szabad munkák
        </Button>
        <Button
          variant="info"
          size="sm"
          className="ml-2"
          onClick={onReservedAssignmentsClick}
        >
          <FontAwesomeIcon icon={faLock} className="mr-2" />
          Általam lefoglalt munkák
        </Button>
      </Row>
      <Row className="mt-2">
        <Form>
          <Form.Group controlId="searchGroup">
            <InputGroup>
              <Form.Control
                size="sm"
                type="text"
                placeholder="keresés"
                value={search}
                onChange={onSearchChange}
              />
              <InputGroup.Append>
                <InputGroup.Text>
                  <FontAwesomeIcon icon={faSearch} />
                </InputGroup.Text>
              </InputGroup.Append>
            </InputGroup>
          </Form.Group>
        </Form>
        <DropdownButton
          variant="info"
          size="sm"
          id="dr"
          title={isAllAssignmentsSelected ? "Összes" : "Még nem értékelt"}
          className="ml-2"
        >
          <Dropdown.Item onSelect={onIsAllAssignmentSelectedChange(true)}>
            Összes
          </Dropdown.Item>
          <Dropdown.Item onSelect={onIsAllAssignmentSelectedChange(false)}>
            Még nem értékelt
          </Dropdown.Item>
        </DropdownButton>
      </Row>
    </Container>
  );
};

export default FilterAssignmentPanel;
