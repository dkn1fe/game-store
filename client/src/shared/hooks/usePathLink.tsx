import { useLocation } from "react-router-dom";

export const usePathLink = () => {
  const links = {
    admin: "Главная",
    products: "Товары",
    chats: "Чаты",
    users: "Добавления",
    edit: "Изменение",
    add: "Добавление",
  };

  const path = useLocation().pathname.split("/");

  const navigateLinks = path
    .filter((item) => item !== "")
    .map((link) => ({
      name: links[link],
      href: link === 'add' || link === 'edit' ? `${path[2]}/${path[3]}` : `/${link}`
    }));

  return navigateLinks
};
