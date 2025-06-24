import axios from "axios";
import { User } from "../Interfaces/User/UserInterface";
import { UserCreate } from "../Interfaces/User/UserCreateInterface";
import { Login } from "../Interfaces/User/LoginInterface";
import { VerifyOtp } from "../Interfaces/User/VerifyOtpInerface";
import { VerifyResponse } from "../Interfaces/User/VerifyResponseInterface";
import api from "./ApiService";

export const createUser = async (user: UserCreate): Promise<User> => {
  try {
    const response = await api.post<User>(`User`, user);
    return response.data as User;
  } catch (error: unknown) {
    handleAxiosError(error, "createUser");
    throw error;
  }
};

export const loginUser = async (login: Login): Promise<boolean> => {
  try {
    const response = await api.post(`Auth/login`, login);
    return (response.status === 200) as boolean;
  } catch (error: unknown) {
    handleAxiosError(error, "loginUser");
    throw error;
  }
};

export const verifyOtp = async (
  verifyOtp: VerifyOtp
): Promise<VerifyResponse> => {
  try {
    const response = await api.post<VerifyResponse>(
      `Auth/verify-otp`,
      verifyOtp
    );
    return response.data as VerifyResponse;
  } catch (error: unknown) {
    handleAxiosError(error, "verifyOtp");
    throw error;
  }
};

const handleAxiosError = (error: unknown, method: string) => {
  if (axios.isAxiosError(error)) {
    console.error(`[${method}] Axios error:`, error.response?.data);
  } else {
    console.error(`[${method}] Unexpected error:`, error);
  }
};
