import { HeaderSheet } from "./HeaderSheet";
import { HeaderSearch } from "./HeaderSearch";
import { HeaderBasket } from "./HeaderBasket";

export const Header = () => {
  return (
    <header>
      <div className="fixed z-10">
        <HeaderSheet />
      </div>
      <div className="container pt-5">
        <div className="flex w-full justify-between items-center">
          <HeaderSearch />
          <HeaderBasket />
        </div>
      </div>
    </header>
  );
};
