
export const getDate = (date:string) => {
    const years =  date.slice(0,4)
    const month =  date.slice(5,7)
    const day = date.slice(8,10)

    return `${day}.${month}.${years}`
}