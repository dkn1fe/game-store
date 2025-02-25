import {
  Button,
  Popover,
  PopoverButton,
  PopoverPanel,
} from "@headlessui/react";
import { AdminType } from "@/entities/types/authType";
import { Settings, LogOut } from "lucide-react";
import { FC } from "react";

interface PopoverListProps {
  data: AdminType;
}

type PopoverListPropsType = FC<PopoverListProps>;

export const PopoverList: PopoverListPropsType = ({ data }) => {
  return (
    <Popover>
      <PopoverButton className="focus:outline-none">
        <img className="w-12 h-12" src={data.img} />
      </PopoverButton>
      <PopoverPanel
        transition
        anchor={{ to: "bottom", offset: -100 }}
        className="min-w-64 max-w- bg-[#2f2f2f] h-auto my-1 rounded-xl shadow-2xl"
      >
        <div className="flex flex-col text-gray-200 p-3">
          <div className="flex flex-col items-left pb-3">
            <h4 className="text-xl font-bold">Email</h4>
            <p>{data.email}</p>
          </div>
          <div className="flex flex-col items-left py-1">
            <h4 className="text-xl font-bold">Role</h4>
            <p className="uppercase">{data.role}</p>
          </div>
          <Button className="flex items-center justify-center gap-3 mt-4 p-3 rounded-lg duration-100 hover:bg-gray-600">
            <Settings size={18} />
            Settings
          </Button>
          <Button className="flex items-center justify-center gap-3 p-3 rounded-lg duration-100 hover:bg-red-600">
            <LogOut size={18} />
            Log Out
          </Button>
        </div>
      </PopoverPanel>
    </Popover>
  );
};
