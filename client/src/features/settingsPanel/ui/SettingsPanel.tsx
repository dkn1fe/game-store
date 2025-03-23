import { CirclePlus, PencilLine, Trash2, RefreshCw } from "lucide-react";

export const SettingsPanel = () => {
  const panelList = [
    { id: 1, title: "Создать", icon: <CirclePlus size={24} color="white" /> },
    { id: 2, title: "Изменить", icon: <PencilLine size={24} color="white" /> },
    { id: 3, title: "Удалить", icon: <Trash2 size={24} color="white" /> },
    { id: 4, title: "Обновить", icon: <RefreshCw size={24} color="white" /> },
  ];

  return (
    <div className="flex items-center gap-8 pl-10 pt-5">
      {panelList.map((item) => (
        <div key={item.id} className="flex gap-2 flex-col items-center cursor-pointer">
          {item.icon}
          <p className="text-white/50 text-lg">{item.title}</p>
        </div>
      ))}
    </div>
  );
};
