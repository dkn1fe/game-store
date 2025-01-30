import { Header } from "@/widgets/header/ui/Header"
import { Outlet } from "react-router-dom"

export const Root = () => {
    return (
        <div className="bg-[#242424] h-screen">
          <header>
            <Header/>
          </header>
          <main className="flex flex-col justify-between">
            <Outlet/>
          </main> 
          <footer></footer>
        </div>
    )
}