import { HeaderPanel } from "@/widgets/header";
import { Outlet } from "react-router-dom";

export const AdminRootPage = () => {
  return (
    <div className="bg-[var(--bg-main-color)] h-screen pb-20 overflow-x-hidden">
      <HeaderPanel />
      <div className="border border-gray-400 w-full mt-4"/>
      <main className="flex flex-col justify-between">
        <Outlet />
      </main>
    </div>
  );
};
