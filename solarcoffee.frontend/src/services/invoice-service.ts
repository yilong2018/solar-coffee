import axios from 'axios';
import { IInvoice } from "../types/Invoice";

//Invoice Service
//Used OrderController
//Provides UI business logic associated with Invoice in our system
export default class InvoiceService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getInvoices(): Promise<IInvoice[]> {
    let result: any = await axios.get(`${this.API_URL}/invoice/`);
    return result.data;
  };

  public async makeNewInvoice(invoice: IInvoice): Promise<boolean> {
    let now = new Date();
    invoice.createdOn = now;
    invoice.updatedOn = now;
    let result: any = await axios.post(`${this.API_URL}/invoice/`, invoice);
    return result.data;
  };

  public async completeInvoice(invoiceId: number): Promise<boolean> {
    let result: any = await axios.patch(`${this.API_URL}/invoice/completed/${invoiceId}`);
    return result.data;
  };
}