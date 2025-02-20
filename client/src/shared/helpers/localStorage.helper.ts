


export const onChangeIsAdmin = (role:'admin' | 'manager') => {
   localStorage.setItem('isAdmin',role)
}

export const onGetIsAdmin = () => {
    const isAdmin = localStorage.getItem('isAdmin')
    return isAdmin === 'admin' || isAdmin === 'manager'
}