import { Location } from "../Interfaces/Location/LocationInterface";
import api from "./ApiService";

export const getLocationByIdLikeString = async (
  id: number
): Promise<string | null> => {
  try {
    const response = await api.get<Location>(`Location/${id}`);
    if (!response.data) {
      throw new Error(`Error fetching location: ${response.statusText}`);
    }

    const locationStr = `${response.data.country}, ${response.data.city}, ${response.data.street} ${response.data.buildingNumber}, "${response.data.gymName}"`;

    return locationStr;
  } catch (error) {
    console.error("Failed to fetch location:", error);
    return null;
  }
};
