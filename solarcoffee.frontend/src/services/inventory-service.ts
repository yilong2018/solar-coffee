import axios from 'axios';
import { IProduct, IProductInventory } from '@/types/Product';

/*
 * Inventory Service
 * Provides UI business logic associsated with product inventory 
 */
export class InventoryService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getInvenotry(): Promise<any> {
    //console.log('getInventory:', this.API_URL)
    let result: any = await axios.get(`${this.API_URL}/inventory/`);
    return result.data;
  }
}
