import { RouterProvider as ReactRouterProvider } from "react-router-dom";

import { router } from "@/app/providers/RouterProvider/config/router"

export const RouterProvider = () => <ReactRouterProvider router={router} />