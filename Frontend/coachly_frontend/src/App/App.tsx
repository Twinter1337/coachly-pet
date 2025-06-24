import { Routes, Route } from "react-router-dom";
import HomePage from "../Pages/Home/HomePage";
import AboutPage from "../Pages/About/AboutPage";
import AuthPage from "../Pages/Auth/AuthPage";
import Header from "../Components/Header/Header";
import { UserProvider } from "../Contexts/User/UserContext";

import "./App.css";
import AccountPage from "../Pages/Account/AccountPage";

const App = () => (
  <UserProvider>
    <div>
      <Header></Header>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/about" element={<AboutPage />} />
        <Route path="/login" element={<AuthPage />} />
        <Route path="/my-account" element={<AccountPage />} />
      </Routes>
    </div>
  </UserProvider>
);

export default App;
