import { useState } from "react";
import NavigationLink from "./Link/NavigationLink";
import { NavigationLinkInfo } from "./Link/NavigationLinkInfo";
import "./Navigation.css";

const Navigation = () => {
  const [linksInfo] = useState<NavigationLinkInfo[]>([
    { labelText: "Home", href: "/" },
    // { labelText: "About", href: "/about" },
    // { labelText: "Contact", href: "/contact" },
    { labelText: "My account", href: "/my-account" },
  ]);

  return (
    <nav className="navigation-container">
      {linksInfo.map((link, index) => (
        <NavigationLink
          key={link.href || index}
          labelText={link.labelText}
          href={link.href}
          isActive={link.isActive}
        />
      ))}
    </nav>
  );
};

export default Navigation;
