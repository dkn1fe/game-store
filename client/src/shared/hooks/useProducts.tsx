import ProductService from "@/entities/api/productService";
import { ProductType } from "@/entities/types/productType";
import { useQuery } from "react-query";


export const useProducts = ({filter = 'all'}:{filter?:string}) => {
    const onGetProducts = async () => await ProductService.getAllProducts();

    const { data,isLoading} = useQuery(["products/getAllProducts"], onGetProducts);
  
    const prodList = data?.filter((item: ProductType) => {
        if (item.status === filter) return item;
        if (filter === "all") return data;
      }) ?? [];

      return {data,prodList,isLoading}

}