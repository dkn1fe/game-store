import { createBrowserRouter } from "react-router-dom";
import { Root } from "@/pages/Root";
import { Home } from "@/pages/Home";
import { Login } from "@/features/login";

export const routes = [
  {
    path: "/",
    element: <Root />,
    children: [
      {
        path: "/",
        element: <Home />,
      },
      {
        path: "/api/login",
        element: <Login/>
      },
    ],
  },
];

export const router = createBrowserRouter(routes);
