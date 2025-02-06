import { createBrowserRouter } from "react-router-dom"
import { Root } from "@/pages/Root"


export const routes = [
    {
        path:'/',
        element:<Root/>,
        children:[
            // {
            //     path:'/',
            //     element:
            // }
        ]
    }
]

export const router = createBrowserRouter(routes)