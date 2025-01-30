import { Input } from "@headlessui/react"
import { Search } from 'lucide-react';

export const HeaderSearch = () => {
    return (
        <div className="relative ml-80">
            <Input className='relative text-gray-400 py-2 px-12 rounded-lg bg-[#383838]' placeholder="Search" />
            <Search className='absolute top-2 left-3' size={20} color = 'gray' fill = 'gray' />
        </div>
    )
}