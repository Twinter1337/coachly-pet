import { User } from "../Interfaces/User/UserInterface";
import api from "./ApiService";

export const getUserById = async (userId: number): Promise<User | null> => {
  try {
    const response = await api.get<User>(`User/${userId}`);
    if (!response.data) {
      throw new Error(`Error fetching user: ${response.statusText}`);
    }
    return response.data as User;
  } catch (error) {
    console.error("Failed to fetch user:", error);
    return null;
  }
};

export const getAllUsers = async (): Promise<User[] | null> => {
  try {
    const response = await api.get<User[]>(`User`);
    if (!response.data) {
      throw new Error(`Error fetching users: ${response.statusText}`);
    }
    return response.data as User[];
  } catch (error) {
    console.error("Failed to fetch users:", error);
    return null;
  }
};
