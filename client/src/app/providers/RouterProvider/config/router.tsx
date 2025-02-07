import { createBrowserRouter } from "react-router-dom"
import { Root } from "@/pages/Root"
import { Home} from "@/pages/Home"


export const routes = [
    {
        path:'/',
        element:<Root/>,
        children:[
            {
                path:'/',
                element:<Home/>
            }
        ]
    }
]

export const router = createBrowserRouter(routes)