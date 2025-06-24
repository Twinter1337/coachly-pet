import { TrainerWithUser } from "../Interfaces/Trainer/TrainerWithUserInterface";
import { Trainer } from "../Interfaces/Trainer/TrainerInterface";
import { User } from "../Interfaces/User/UserInterface";
import api from "../Services/ApiService";
import { getAllUsers } from "./UserService";

export const getTrainersWithUser = async (): Promise<
  TrainerWithUser[] | null
> => {
  try {
    const trainersResponse = await api.get<Trainer[]>("Trainer");
    if (!trainersResponse.data) {
      throw new Error(
        `Error fetching trainers: ${trainersResponse.statusText}`
      );
    }
    const trainers = trainersResponse.data as Trainer[];

    const userResponse = await getAllUsers();

    let trainersWithUser: TrainerWithUser[] = [];
    trainers.forEach((t) => {
      let userForTrainer = userResponse?.find((u) => u.id === t.userId);
      if (userForTrainer) {
        trainersWithUser.push({ user: userForTrainer, trainer: t });
      }
    });

    return trainersWithUser;
  } catch (error) {
    console.error("Failed to fetch response:", error);
    return null;
  }
};
