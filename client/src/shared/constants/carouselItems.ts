import robux from "@/assets/carousel/robux.webp";
import standoff from "@/assets/carousel/gold.webp";
import pubg from "@/assets/carousel/uc.webp";

export const carouselItems = [
  {
    id: 1,
    title: "PUBG",
    link: "/items/pubg",
    desc: "A premium currency that is purchased with real money.Used to buy elite skins, crates, battle passes, and exclusive items.",
    img: pubg,
  },
  {
    id: 2,
    title: "STANDOFF",
    desc: "The primary premium currency in Standoff 2Used to purchase elite weapon skins, knives, gloves, and other rare items",
    link: "/items/standoff",
    img: standoff,
  },
  {
    id: 1,
    title: "ROBLOX",
    desc: "The primary currency in Roblox, used for purchasing in-game items, accessories, avatar customizations, and game passes.",
    link: "/items/robux",
    img: robux,
  },
];
