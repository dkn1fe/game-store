import { HeaderSheet } from './HeaderSheet'
import { HeaderSearch } from './HeaderSearch'
import { HeaderBasket } from './HeaderBasket'

export const Header = () => {
    return (
        <>
            <div className='absolute z-10'>
                <HeaderSheet />
            </div>
            <div className='container pt-5'>
                <div className='flex justify-between'>
                    <HeaderSearch />
                    <HeaderBasket />
                </div>
            </div>
        </>
    )
}