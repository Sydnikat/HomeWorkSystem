import React from "react";
import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from "react-router-dom";
import "./App.css";
import StudentHome from "./components/StudentHome";
import TeacherHome from "./components/TeacherHome";
import Signin from "./components/Signin";
import Signup from "./components/Signup";
import { RootState } from "./store/rootReducer";
import { useSelector } from "react-redux";

const App: React.FC = () => {
  const user = useSelector((state: RootState) => state.userReducer.user);
  return (
    <Router>
      <Switch>
        <Route
          exact
          path="/"
          render={() =>
            !user ? (
              <Redirect to="/signin" />
            ) : user.role === "student" ? (
              <Redirect to="/student" />
            ) : user.role === "teacher" ? (
              <Redirect to="/teacher/groups" />
            ) : (
              <Redirect to="/signin" />
            )
          }
        />
        <Route exact path="/signin" component={Signin} />
        <Route exact path="/signup" component={Signup} />
        <Route
          path="/student"
          render={() =>
            user && user.role === "student" ? (
              <StudentHome />
            ) : (
              <Redirect to="/signin" />
            )
          }
        />
        <Route
          exact
          path="/teacher"
          render={() => <Redirect to="/teacher/groups" />}
        />
        <Route
          path="/teacher"
          render={() =>
            user && user.role === "teacher" ? (
              <TeacherHome />
            ) : (
              <Redirect to="/signin" />
            )
          }
        />
      </Switch>
    </Router>
  );
};

export default App;
