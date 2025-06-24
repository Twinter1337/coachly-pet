import React, { useState } from "react";
import * as userService from "../../../Services/AuthService";
import { UserRole } from "../../../Interfaces/Enums/UserRole";
import "./AuthForm.css";

interface Props {
  email: string;
  setEmail: (email: string) => void;
  setStep: (step: number) => void;
}

const RegisterForm = ({ email, setEmail, setStep }: Props) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [password, setPassword] = useState("");
  const [phone, setPhone] = useState("");
  const [active, setActive] = useState<"client" | "trainer">("client");

  const handleSignUp = async () => {
    try {
      const response = await userService.createUser({
        firstName,
        lastName,
        email,
        passwordHash: password,
        phone,
        UserRole: active === "client" ? UserRole.Client : UserRole.Trainer,
      });
      if (response) setStep(2);
    } catch {
      console.error("Registration failed");
    }
  };

  return (
    <div className="auth-options">
      <h2>Sign Up</h2>
      <div className="sign-up-options">
        <div
          className={`option ${active === "client" ? "active-option" : ""}`}
          onClick={() => setActive("client")}
        >
          Client
        </div>
        <div
          className={`option ${active === "trainer" ? "active-option" : ""}`}
          onClick={() => setActive("trainer")}
        >
          Trainer
        </div>
      </div>
      <div className="sign-up">
        <input
          className="auth-input"
          type="text"
          placeholder="First Name"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          required
        />
        <input
          className="auth-input"
          type="text"
          placeholder="Last Name"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          required
        />
        <input
          className="auth-input"
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <input
          className="auth-input"
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <input
          className="auth-input"
          type="tel"
          pattern="^\+380\d{9}$"
          placeholder="+380XXXXXXXXX"
          value={phone}
          onChange={(e) => setPhone(e.target.value)}
          required
        />
        <button className="log-in-button" onClick={handleSignUp}>
          Sign Up
        </button>
      </div>
    </div>
  );
};

export default RegisterForm;
