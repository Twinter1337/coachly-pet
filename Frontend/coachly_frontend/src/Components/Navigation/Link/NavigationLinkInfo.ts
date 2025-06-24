export type NavigationLinkInfo = {
  labelText: string;
  href: string;
  isActive?: boolean;
  onClick?: (href: string) => void;
};
