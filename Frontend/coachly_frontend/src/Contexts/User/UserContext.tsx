import React, {
  createContext,
  useContext,
  useEffect,
  useState,
  ReactNode,
} from "react";
import { User } from "../../Interfaces/User/UserInterface";
import { Trainer } from "../../Interfaces/Trainer/TrainerInterface";

interface UserContextType {
  user: User | null;
  trainer: Trainer | null;
  isAuthorized: boolean;
  setUser: (user: User | null, token: string) => void;
  setTrainer: (trainer: Trainer | null) => void;
  logout: () => void;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};

export const UserProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUserState] = useState<User | null>(null);
  const [trainer, setTrainerState] = useState<Trainer | null>(null);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      try {
        setUserState(JSON.parse(storedUser));
      } catch (e) {
        console.warn("Error while reading user from localStorage", e);
      }
    }
  }, []);

  const setUser = (newUser: User | null, token?: string) => {
    setUserState(newUser);
    if (newUser && token) {
      localStorage.setItem("user", JSON.stringify(newUser));
      localStorage.setItem("token", token);
    } else {
      localStorage.removeItem("user");
      localStorage.removeItem("token");
    }
  };

  const setTrainer = (newTrainer: Trainer | null) => {
    setTrainerState(newTrainer);
    if (newTrainer) {
      localStorage.setItem("trainer", JSON.stringify(newTrainer));
    } else {
      localStorage.removeItem("trainer");
    }
  };

  const logout = () => {
    setUser(null);
    setTrainer(null);
  };

  return (
    <UserContext.Provider
      value={{
        user,
        setUser,
        trainer,
        setTrainer,
        logout,
        isAuthorized: !!user,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};
