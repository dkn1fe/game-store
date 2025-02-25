import { linksMenu } from "@/shared/constants/linksMenu";
import { Button } from "@headlessui/react";

export const LinksMenu = () => {
  return (
    <div className="flex justify-center items-center pt-36">
      <div className="flex flex-col justify-center">
        <h3 className="text-[var(--white)] font-medium text-5xl">
          Добро пожаловать в админ панель
        </h3>
        <span className="text-[#f08c00] font-medium pt-4 text-center text-3xl">
          Game-Store
        </span>
        <div className="grid grid-cols-2 pt-10 gap-4">
          {linksMenu &&
            linksMenu.map((item) => (
              <Button
                className="py-2 px-10 font-medium bg-[#e8590c] rounded-lg text-[var(--white)] flex justify-center"
                key={item.id}
              >
                {item.title}
              </Button>
            ))}
          <Button className="col-span-2 py-2 px-10 font-medium border border-[#e8590c] rounded-lg text-[var(--white)] flex justify-center">
            Перейти на Сайт
          </Button>
        </div>
      </div>
    </div>
  );
};