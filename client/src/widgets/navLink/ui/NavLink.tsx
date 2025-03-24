import { usePathLink } from "@/shared/hooks/usePathLink";
import { ChevronRight } from "lucide-react";
import { Link } from "react-router-dom";

export const NavLink = () => {
  const navigateLink = usePathLink();
  return (
    <div className="flex items-center gap-3">
      {navigateLink.length !== 1 &&
        navigateLink.map((item, index) => (
          <Link
            className="text-gray-300 flex items-center gap-3 text-xl hover:text-gray-400"
            to={item.href}
          >
            {item.name}
            {index !== navigateLink.length - 1 && (
              <ChevronRight size={20} className="pt-1" />
            )}
          </Link>
        ))}
    </div>
  );
};
