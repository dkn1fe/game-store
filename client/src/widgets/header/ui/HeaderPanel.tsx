import { PopoverList } from "@/features/popover";
import { useAdminStore } from "@/shared/zustand/adminStore";

export const HeaderPanel = () => {
  const adminData = useAdminStore((state: any) => state.adminInfo);

  const adminInfo = adminData
    ? adminData
    : JSON.parse(localStorage.get("admin-data"));

  return (
    <>
      <div className="flex justify-between items-center py-5  px-10">
        <h3 className="text-5xl font-medium text-[var(--white)]">Game-Store</h3>
        <div className="flex items-center gap-3">
          {adminInfo && <PopoverList data={adminInfo} />}
        </div>
      </div>
    </>
  );
};
