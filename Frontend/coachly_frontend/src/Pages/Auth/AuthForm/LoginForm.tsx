import React from "react";
import * as userService from "../../../Services/AuthService";
import "./AuthForm.css";

interface Props {
  email: string;
  password: string;
  setEmail: (email: string) => void;
  setPassword: (password: string) => void;
  setStep: (step: number) => void;
}

const LoginForm = ({
  email,
  password,
  setEmail,
  setPassword,
  setStep,
}: Props) => {
  const handleLogin = async () => {
    try {
      // const response = await
      userService.loginUser({ email, password });
      // if (response) {
      setStep(2);
      // }
    } catch {
      console.error("Login failed");
    }
  };

  return (
    <div className="auth-options">
      <h2>Login</h2>
      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin} className="log-in-button">
        Log In
      </button>
      <button className="link-button-sign-up" onClick={() => setStep(1)}>
        Don`t have an account yet? Sign up
      </button>
    </div>
  );
};

export default LoginForm;
