import { productType } from "../types/productType";
import { baseUrl } from "./api";

class ProductService {
  static async getAllProducts() {
    const { data } = await baseUrl.get<productType>("Products");
    return data;
  }
}

export default ProductService;
