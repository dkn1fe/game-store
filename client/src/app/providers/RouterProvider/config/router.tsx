import { createBrowserRouter } from "react-router-dom";
import { Root } from "@/pages/Root";
import { Home } from "@/pages/Home";
import { Login } from "@/features/login";
import { AdminRootPage } from "@/pages/AdminRoot";
import { AdminLinksPage } from "@/pages/AdminLinks";
import { PrivateRoute } from "./privateRouter";

export const mainRoutes = [
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
        element: <Login />,
      },
    ],
  },
];

export const adminRoutes = [
  {
    path: "/admin",
    element: <AdminRootPage />,
    children: [
      {
        path: "/admin",
        element: (
          <PrivateRoute>
            <AdminLinksPage />
          </PrivateRoute>
        ),
      },
    ],
  },
];

const routes = mainRoutes.concat(adminRoutes);

export const router = createBrowserRouter(routes);
