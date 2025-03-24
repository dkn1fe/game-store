import { Loading } from "@/features/loading";
import { Checkbox } from "@headlessui/react";
import { getDate } from "@/shared/utils/utils";
import { productsSettingsInfo } from "@/shared/constants/productItems";
import { useProducts } from "@/shared/hooks/useProducts";

export const ProductsList = () => {
  const { data, isLoading } = useProducts({});

  return (
    <>
      <Loading isLoading={isLoading} />
      <div className="grid grid-cols-7 items-center p-10 gap-4">
        {productsSettingsInfo.map((item, index) => (
          <div key={item.id} className="flex items-center gap-4">
            <span className="text-white/50">{item.title}</span>
            {index !== productsSettingsInfo.length - 1 && (
              <span className="h-6 w-px bg-gray-500"></span>
            )}
          </div>
        ))}
      </div>

      <div className="grid grid-cols-1 px-10 gap-8">
        {data &&
          data.map((item) => (
            <div
              key={item.id}
              className="w-full h-16 rounded-xl border-2 p-3 border-white/30"
            >
              <div className="grid grid-cols-7 items-center gap-4">
                <div className="flex items-center gap-4">
                  <Checkbox className="group size-5 rounded-md bg-white/10 p-1 ring-1 ring-white/15 ring-inset data-[checked]:bg-white" />
                  <p className="text-white/70 truncate">{item.id.slice(0, 10)}...</p>
                </div>
                <img className="w-10 h-10 rounded-xl" src={item.img} alt={item.title} />
                <p className="text-white/70 truncate">{item.title}</p>
                <p className="text-white/70">{item.price}р</p>
                <Checkbox className="group size-5 rounded-md bg-white/10 ml-10 ring-1 ring-white/15 ring-inset data-[checked]:bg-white" />
                <p className="text-white/70 ml-10">{getDate(item.creationDate)}</p>
              </div>
            </div>
          ))}
      </div>
    </>
  );
};