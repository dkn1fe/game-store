import { ProductType } from "../types/productType";
import { baseUrl } from "./api";

class ProductService {
  static async getAllProducts() {
    try {
      const { data } = await baseUrl.get<ProductType[]>("Products");
      return data?.length ? data : [];
    } catch(err) {
      console.error(err);
      throw err;
    }
  }
}

export default ProductService;
