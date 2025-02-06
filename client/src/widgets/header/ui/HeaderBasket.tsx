import { Bell } from "lucide-react"
import { ShoppingBasket } from "lucide-react"

export const HeaderBasket = () => {
    return (
        <div className="flex gap-8 cursor-pointer">
            <Bell color='gray' fill='gray' />
            <ShoppingBasket color='gray' fill='gray' />
        </div>
    )
}