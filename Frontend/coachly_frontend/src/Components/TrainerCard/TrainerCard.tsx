import { TrainerWithUser } from "../../Interfaces/Trainer/TrainerWithUserInterface";
import { getTrainerSpecializationsByTrainerId } from "../../Services/TrainerSpecializationService";
import { useEffect, useState } from "react";
import "./TrainerCard.css";
import { Specialization } from "../../Interfaces/Specialization/SpecializationInterface";
import { getLocationByIdLikeString } from "../../Services/LocationService";
import Card from "../CardContainer/Card";

interface TrainerCardProps extends TrainerWithUser {
  openModal?: () => void;
}

const TrainerCard = ({ trainer, user, openModal }: TrainerCardProps) => {
  const [specializations, setSpecializations] = useState<Specialization[]>([]);
  const [location, setLocation] = useState<string>("");

  useEffect(() => {
    const fetchSpecializations = async () => {
      try {
        const specializationsList = await getTrainerSpecializationsByTrainerId(
          trainer.id
        );
        if (specializationsList) {
          setSpecializations(specializationsList);
        }
      } catch (error) {
        console.error("Failed to fetch specializations:", error);
      }
    };

    const fetchLocation = async () => {
      try {
        const loc = await getLocationByIdLikeString(trainer.locationId!);
        setLocation(loc || "");
      } catch (error) {
        console.error("Failed to fetch location:", error);
      }
    };

    fetchSpecializations();
    fetchLocation();
  }, [trainer.id]);

  return (
    <Card className="trainer-card">
      <div className="trainer-card-header">
        <h2 className="trainer-name">
          {user.firstName} {user.lastName}
        </h2>
        <p className="trainer-rating">⭐ {trainer.avgRating.toFixed(1)}</p>
      </div>

      <div className="trainer-card-body">
        {specializations.length > 0 && (
          <p className="specializations-line">
            {specializations.map((spec) => spec.name).join(", ")}
          </p>
        )}

        {trainer.bio && (
          <p className="trainer-bio">
            <strong>About:</strong> {trainer.bio}
          </p>
        )}

        {location && (
          <p className="trainer-location">
            <strong>Location:</strong> {location}
          </p>
        )}
      </div>

      <div className="trainer-controls">
        <button
          className="signup-button"
          onClick={() => openModal && openModal()}
        >
          Sign up for session
        </button>
      </div>
    </Card>
  );
};

export default TrainerCard;
