import { onChangeIsAdmin } from "@/shared/helpers/localStorage.helper";
import { instance } from "./api";
import { LoginType } from "../types/authType";

class AuthService {
  static async login({ login, password }:LoginType) {
    const { data } = await instance.post("login", { login, password });
    onChangeIsAdmin(data?.role)
    return data;
  }
}

export default AuthService;
