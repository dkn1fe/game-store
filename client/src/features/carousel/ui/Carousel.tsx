import { carouselItems } from "@/shared/constants/carouselItems";
import { ShoppingBasket } from "lucide-react";
import { Link } from "react-router-dom";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./carousel.css";

export const Carousel = () => {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3000,
    fade: true,
  };

  return (
    <Slider {...settings} className="w-[90%] ml-10 mt-10 h-[350px]">
      {carouselItems.map((item) => (
        <div className="relative">
          <div className="absolute inset-0 bg-black bg-opacity-50 z-10"></div>
          <img
            key={item.id}
            style={{
              backgroundImage: `url(${item.img})`,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
            className="relative rounded-xl w-full h-[350px] object-cover flex items-center justify-center"
          />
          <div className="absolute top-10 left-10 z-10 flex flex-col items-left">
            <h3 className="font-bold text-2xl text-white">{item.title}</h3>
            <p className="max-w-[400px] text-gray-300 font-medium pt-8">
              {item.desc}
            </p>
            <div className="relative mt-10">
              <Link
                to={item.link}
                className="text-[#ee8efe] max-w-[200px] font-bold flex items-center gap-4 bg-[var(--white)] rounded-xl p-3 px-8"
              >
                <ShoppingBasket color="#da00fd" />
                Order Now
              </Link>
            </div>
          </div>
        </div>
      ))}
    </Slider>
  );
};
