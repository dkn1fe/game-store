import { ProductType } from "../types/productType";
import { instance } from "./api";

class ProductService {
  static async getAllProducts() {
    try {
      const { data } = await instance.get<ProductType[]>("Products");
      return data?.length ? data : [];
    } catch(err) {
      console.error(err);
      throw err;
    }
  }
}

export default ProductService;
