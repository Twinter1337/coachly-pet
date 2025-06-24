import { TrainerSpecialization } from "../Interfaces/TrainerSpecializations/TrainerSpecializationInterface";
import { Specialization } from "../Interfaces/Specialization/SpecializationInterface";
import api from "./ApiService";

export const getTrainerSpecializationsByTrainerId = async (
  trainerId: number
): Promise<Specialization[] | null> => {
  try {
    const response = await api.get<TrainerSpecialization[]>(
      "TrainerSpecialization"
    );

    if (!response.data) {
      throw new Error(
        `Error fetching trainer specializations: ${response.statusText}`
      );
    }

    const specializationsByTrainerId = response.data.filter(
      (ts) => ts.trainerId === trainerId
    );

    if (specializationsByTrainerId.length === 0) {
      console.warn(`No specializations found for trainer with ID ${trainerId}`);
      return null;
    }

    const specializations = await api.get<Specialization[]>("Specialization");

    if (!specializations.data) {
      throw new Error(
        `Error fetching specializations: ${specializations.statusText}`
      );
    }

    let trainerSpecializationList: Specialization[] = [];

    specializationsByTrainerId.forEach((ts) => {
      const specialization = specializations.data.find(
        (s) => s.id === ts.specalizationId
      );
      if (specialization) {
        trainerSpecializationList.push(specialization);
      }
    });

    return trainerSpecializationList;
  } catch (error) {
    console.error("Failed to fetch trainer specializations:", error);
    return null;
  }
};
