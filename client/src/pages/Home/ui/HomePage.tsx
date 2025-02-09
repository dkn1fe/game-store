import { Carousel } from "@/features/carousel";
import { ProductFilter } from "@/features/product";

export const Home = () => {
  return (
    <div className="container ml-80">
      <Carousel />
      <ProductFilter />
    </div>
  );
};
