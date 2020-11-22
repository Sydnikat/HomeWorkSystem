import {IUserResponse} from "../models/user";
import {axiosInstance} from "./config/axios";
import {userServiceUrl} from "./config/url";


export interface UserService {
  getUsers(): Promise<IUserResponse[]>;
}

export const userService: UserService = {
  async getUsers() {
    try {
      const response = await axiosInstance.get(`${userServiceUrl}`);
      if (response.status === 200) {
        return  response.data as IUserResponse[];
      }
      return [];
    } catch (err) {
      console.log(err);
      return [];
    }
  }
};