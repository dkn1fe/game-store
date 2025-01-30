import { Button } from '@headlessui/react'
import { headerSheet, headerSheetCategory } from '@/shared/constants/headerSheet'
import { Link } from 'react-router-dom'
import { Play } from 'lucide-react';
import logo from '@/assets/header/logo.png'

export const HeaderSheet = () => {
    return (
        <div className="flex flex-col bg-[#2d2d2d] h-screen w-80">
            <div className='p-5'>
                <div className="flex items-center gap-3">
                    <img src={logo} />
                    <div className='flex flex-col text-md font-bold'>
                        <h3 className='text-[#DA00FE]'>GAME</h3>
                        <h3 className='pl-3 text-[#A100ED]'>STORE</h3>
                    </div>
                </div>
                <div className='text-center pt-8'>
                    <Button className='px-10 py-2 bg-[#8A8A8A] rounded-xl text-white'>Catalog</Button>
                </div>
                <div className='flex flex-col gap-3  pt-8'>
                    {headerSheet.map(item => (
                        <Link className='text-white font-bold text-lg' to={item.link}>{item.title}</Link>
                    ))}
                </div>
                <div className='flex flex-col justify-center pt-10 gap-4'>
                    <div className='flex justify-between items-center'>
                        <h4 className='text-[#CBCBCB]'>Category</h4>
                        <Play color='gray' fill='gray' />
                    </div>
                    {headerSheetCategory.map(item => (
                        <Link className='text-[#8A8A8A] text-xl font-bold' to={item.link}>{item.title}</Link>
                    ))}
                </div>
            </div>
        </div>
    )
}