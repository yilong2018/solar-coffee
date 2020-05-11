import axios from 'axios'
import {IProduct} from '@/types/Product';


export class ProductService {
  API_URL = process.env.VUE_APP_API_URL;
  
  async archive(productId: number){
    let result: any = await axios.patch(
      `${this.API_URL}/product/${productId}`
    );
    return result.data;
  }

  async save(newProduct: IProduct) {
    let result = await axios.post(`${this.API_URL}/product/`,newProduct);
    return result.data;
  }
}

