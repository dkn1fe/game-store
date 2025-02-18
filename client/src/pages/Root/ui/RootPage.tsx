import { Header } from "@/widgets/header/ui/Header"
import { Outlet } from "react-router-dom"

export const Root = () => {
    return (
        <div className="bg-[var(--bg-main-color)] h-screen pb-20 overflow-x-hidden">
            <Header/>
          <main className="flex flex-col justify-between">
            <Outlet/>
          </main> 
          <footer></footer>
        </div>
    )
}