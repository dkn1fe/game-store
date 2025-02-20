import { onChangeIsAdmin } from "@/shared/helpers/localStorage.helper";
import { instance } from "./api";

class AuthService {
  static async login({ login, password }: { login: string; password: string }) {
    const { data } = await instance.post("login", { login, password });
    onChangeIsAdmin(data?.role)
    return data;
  }
}

export default AuthService;
