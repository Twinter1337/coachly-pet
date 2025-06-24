import { TrainerAvailability } from "../Interfaces/TrainerAvailability/TrainerAvailability";
import api from "./ApiService";

export const getTrainerSheduleByTrainerId = async (
  trainerId: number
): Promise<TrainerAvailability[] | null> => {
  try {
    const response = await api.get<TrainerAvailability[]>(
      "TrainerAvailability"
    );
    if (!response.data) {
      throw new Error(
        `Error fetching trainer schedule: ${response.statusText}`
      );
    }
    const resAsClass = response.data as TrainerAvailability[];
    const filtered = resAsClass.filter((t) => t.trainerId === trainerId);

    return filtered;
  } catch (error) {
    console.error("Failed to fetch response:", error);
    return null;
  }
};
