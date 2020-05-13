import Axios from 'axios';

/*
*/
export class OrderService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getOrders(): Promise<any> {
    let result: any = await Axios.get(`${this.API_URL}/order/`);
    return result.data;
  }

  public async makeOrderComplete(id: number): Promise<any> {
    let result: any = await Axios.patch(`${this.API_URL}/order/complete/${id}`);
    return result.data;
  }
}