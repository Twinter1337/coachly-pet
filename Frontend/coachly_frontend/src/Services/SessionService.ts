import { Session } from "../Interfaces/Session/SesionInterface";
import api from "./ApiService";

export const getSessionsByTrainerId = async (
  trainerId: number
): Promise<Session[] | null> => {
  try {
    const response = await api.get<Session[]>("Session");
    if (!response.data) {
      throw new Error(`error fetching sessions: ${response.statusText}`);
    }

    const resAsClass = response.data as Session[];
    const filtered = resAsClass.filter((s) => s.trainerId === trainerId);

    return filtered;
  } catch (error) {
    console.error(`error fetching data: ${error}`);
    return null;
  }
};
