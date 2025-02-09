import { productButtonFilter } from "@/shared/constants/productFilter";
import { Button } from "@headlessui/react";
import { useState } from "react";
import { ProductList } from "./ProductList";

export const ProductFilter = () => {
  const [chooseFilter, setChooseFilter] = useState("all");

  return (
    <div className="pl-10 pt-10">
      <h3 className="text-white text-3xl">Product</h3>
      <div className="flex items-center gap-4 pt-4">
        {productButtonFilter.map((item) => (
          <Button
            className={`${
              item.label === chooseFilter &&
              "bg-[#a100ed] border-none duration-100"
            } bg-transparent p-1 px-4 text-white rounded-2xl border border-gray-400`}
            onClick={() => setChooseFilter(item.label)}
            key={item.id}
          >
            {item.title}
          </Button>
        ))}
      </div>
      <ProductList filter={chooseFilter} />
    </div>
  );
};
