import axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import { store } from "../../store/store";

const accessToken = store.getState().userReducer.user?.accessToken || "";
const axiosRequestConfig: AxiosRequestConfig = {
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    Authorization: `Bearer ${accessToken}`,
  },
};

export const axiosInstance: AxiosInstance = axios.create(axiosRequestConfig);
