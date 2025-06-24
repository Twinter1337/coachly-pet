import { Link } from "react-router-dom";
import { NavigationLinkInfo } from "./NavigationLinkInfo";
import "./NavigationLink.css";

const NavigationLink = ({
  labelText,
  href,
  isActive,
  onClick,
}: NavigationLinkInfo) => {
  const handleClick = (e: React.MouseEvent) => {
    if (!onClick) return;

    onClick(href);
  };

  return (
    <Link
      to={href}
      className={`navigation-link ${isActive ? "active" : ""}`}
      onClick={handleClick}
    >
      {labelText}
    </Link>
  );
};

export default NavigationLink;
