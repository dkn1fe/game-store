import { CirclePlus } from "lucide-react";
import { Button } from "@headlessui/react";
import { FC } from "react";
import { useQuery } from "react-query";
import { ProductType } from "@/entities/types/productType";
import ProductService from "@/entities/api/productService";

interface ProductListProps {
  filter: string;
}

type ProductListPropsType = FC<ProductListProps>;

export const ProductList: ProductListPropsType = ({ filter }) => {
  const onGetProducts = async () => await ProductService.getAllProducts();

  const { data } = useQuery(["products/getAllProducts"], onGetProducts);

  const prodList = data?.filter((item: ProductType) => {
      if (item.status === filter) return item;
      if (filter === "all") return data;
    }) ?? [];

  return (
    <div className="grid grid-cols-4 gap-10 pt-16 pr-24">
      {prodList.map((item: ProductType) => (
          <div
            key={item.productId}
            className="relative bg-[var(--bg-card)] h-[260px] text-center rounded-lg shadow-xl"
          >
            <img
              className="absolute -top-16 left-[30%] w-32 h-32 mt-10 m-auto"
              src={item.imgSrc}
            />
            <h3 className="text-xl pt-32 text-[var(--white)]">{item.title}</h3>
            <p className="text-gray-300">{item.game}</p>
            <p className="text-[#9216a7] font-bold text-lg">${item.price}</p>
            <Button className="flex gap-3 items-center pt-3 text-gray-300 m-auto">
              <CirclePlus color="black" fill="gray" />
              Add to Cart
            </Button>
          </div>
        ))}
    </div>
  );
};
