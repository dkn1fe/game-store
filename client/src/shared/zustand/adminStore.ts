import { AdminType } from "@/entities/types/authType";
import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

export const useAdminStore = create(
  persist(
    (set) => ({
      adminInfo: null,
      setAdminInfo: (adminInfo: AdminType | null) => set({ adminInfo }),
    }),
    {
      name: "admin-data",
      storage: createJSONStorage(() => localStorage),
    }
  )
);
