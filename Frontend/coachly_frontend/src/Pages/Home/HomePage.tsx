import Page from "../Page/Page";
import "./HomePage.css";
import { useEffect, useState } from "react";
import * as trainerService from "../../Services/TrainerService";
import { TrainerWithUser } from "../../Interfaces/Trainer/TrainerWithUserInterface";
import TrainerCard from "../../Components/TrainerCard/TrainerCard";
import { Specialization } from "../../Interfaces/Specialization/SpecializationInterface";
import * as specializationService from "../../Services/SpecializationService";
import SpecializationCard from "../../Components/SpecializationCard/SpecializationCard";
import TrainerShedule from "../../Components/ModalWindow/TrainerShedule/TrainerShedule";

const HomePage = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const [trainers, setTrainers] = useState<TrainerWithUser[]>([]);
  const [filteredTrainers, setFilteredTrainers] = useState<TrainerWithUser[]>(
    []
  );
  const [specializations, setSpecializations] = useState<Specialization[]>([]);

  const [selectedTrainer, setSelectedTrainer] =
    useState<TrainerWithUser | null>(null);

  useEffect(() => {
    const fetchTrainersWithUser = async () => {
      try {
        const trainers = await trainerService.getTrainersWithUser();
        if (trainers) {
          setTrainers(trainers);
          setFilteredTrainers(trainers);
        } else {
          console.error("No users found or error fetching trainers.");
        }
      } catch (error) {
        console.error("Error fetching trainers:", error);
      }
    };

    fetchTrainersWithUser();
  }, []);

  useEffect(() => {
    const fetchSpecializations = async () => {
      try {
        const specializationsList =
          await specializationService.getSpecializations();
        if (specializationsList) {
          setSpecializations(specializationsList);
        } else {
          console.error(
            "No specializations found or error fetching specializations."
          );
        }
      } catch (error) {
        console.error("Error fetching specializations:", error);
      }
    };

    fetchSpecializations();
  }, []);

  useEffect(() => {
    if (!searchQuery.trim()) {
      setFilteredTrainers(trainers);
      return;
    }

    const query = searchQuery.toLowerCase().trim();
    const queryParts = query.split(" ").filter(Boolean);

    const filtered = trainers.filter(({ user }) => {
      const first = user.firstName.toLowerCase();
      const last = user.lastName.toLowerCase();

      if (queryParts.length === 1) {
        return first.includes(queryParts[0]) || last.includes(queryParts[0]);
      }

      if (queryParts.length >= 2) {
        const [q1, q2] = queryParts;
        return (
          (first.includes(q1) && last.includes(q2)) ||
          (first.includes(q2) && last.includes(q1))
        );
      }

      return false;
    });

    setFilteredTrainers(filtered);
  }, [searchQuery, trainers]);

  return (
    <Page className="home-page">
      <TrainerShedule
        isOpen={selectedTrainer !== null}
        onClose={() => setSelectedTrainer(null)}
        trainer={selectedTrainer!}
      />

      <div className="home-page-heading-container">
        <h1>Look for sports coaches online!</h1>
        <p>Sign up for training from the comfort of your home</p>
        <div className="search-container">
          <input
            className="search-input"
            type="text"
            placeholder="Enter the name or lastname of trainer..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
        </div>
      </div>

      <div className="home-page-body-container">
        <div className="home-page-specializations-container">
          <h2>Specializations:</h2>
          <div className="horizontal-line" />
          <div className="specializations-list">
            {specializations.length > 0 ? (
              specializations.map((spec, index) => (
                <SpecializationCard key={index} name={spec.name} />
              ))
            ) : (
              <p>No specializations available.</p>
            )}
          </div>
        </div>

        <div className="home-page-trainers-container">
          <h2>Trainers:</h2>
          <div className="horizontal-line" />
          <div className="trainers-list">
            {filteredTrainers.length > 0 ? (
              filteredTrainers.map((trainerWithUser) => (
                <TrainerCard
                  key={trainerWithUser.trainer.id}
                  trainer={trainerWithUser.trainer}
                  user={trainerWithUser.user}
                  openModal={() => setSelectedTrainer(trainerWithUser)}
                />
              ))
            ) : (
              <p>No trainers found.</p>
            )}
          </div>
        </div>
      </div>
    </Page>
  );
};

export default HomePage;
