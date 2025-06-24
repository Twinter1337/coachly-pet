import api from "./ApiService";
import { Specialization } from "../Interfaces/Specialization/SpecializationInterface";

export const getSpecializations = async (): Promise<
  Specialization[] | null
> => {
  try {
    const specializationsResponse = await api.get<Specialization[]>(
      "Specialization"
    );
    if (!specializationsResponse.data) {
      throw new Error(
        `Error fetching specializations: ${specializationsResponse.statusText}`
      );
    }

    return specializationsResponse.data as Specialization[];
  } catch (error) {
    console.error("Failed to fetch users:", error);
    return null;
  }
};

export const getSpecializationById = async (
  id: number
): Promise<Specialization | null> => {
  try {
    const specializationResponse = await api.get<Specialization>(
      `Specialization/${id}`
    );
    if (!specializationResponse.data) {
      throw new Error(
        `Error fetching specialization: ${specializationResponse.statusText}`
      );
    }

    return specializationResponse.data as Specialization;
  } catch (error) {
    console.error("Failed to fetch specialization:", error);
    return null;
  }
};
export function getTrainerSpecializationsByTrainerId(id: number) {
  throw new Error("Function not implemented.");
}
