import axios from "axios";

export const instance = axios.create({
  baseURL: "http://185.255.134.111:5000/api/",
  timeout: 3000,
});
