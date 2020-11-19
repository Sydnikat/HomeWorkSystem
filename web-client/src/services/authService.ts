import axios from "axios";
import { IUser } from "../models/user";
import { authServiceUrl } from "./config/url";

interface AuthService {
  signin(userName: string, password: string): Promise<IUser | undefined>;
  signup(
    userName: string,
    password: string,
    repassword: string,
    userFullName: string,
    role: string
  ): Promise<boolean>;
}

export const authService: AuthService = {
  async signin(userName: string, password: string): Promise<IUser> {
    return axios
      .post(`${authServiceUrl}/signin`, { userName, password })
      .then((res) => {
        if (res.status === 200) {
          return res.data as IUser;
        }
        return (undefined as unknown) as IUser;
      })
      .catch((err) => {
        console.log(err);
        return (undefined as unknown) as IUser;
      });
  },

  async signup(
    userName: string,
    password: string,
    repassword: string,
    userFullName: string,
    role: string
  ) {
    return axios
      .post(`${authServiceUrl}/signup`, {
        userName,
        password,
        repassword,
        userFullName,
        role,
      })
      .then((res) => {
        if (res.status === 201) {
          return true;
        }

        return false;
      })
      .catch((err) => {
        console.log(err);
        return false;
      });
  },
};
