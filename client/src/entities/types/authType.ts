
export interface LoginType{
    login:string, 
    password:string,
}

export interface AdminType{
    email:string,
    id:string,
    img:string,
    role:'admin' | 'manager',
    username:string
}