<template>
  <div>
    <h1 id="invoiceTitle">
      Create Invoice
    </h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep === 1">
      <h2>Step1: Select Customer</h2>
      <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select v-modal="selectedCustomerId" class="invoiceCustomers" id="customer">
          <option disabled value="">Please select a Csutomer</option>
          <option v-for="c in customers" :key="c.id ">{{ c.firstName + " " + c.lastName }}</option>
        </select>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 2">

    </div>
    <div class="invoice-step" v-if="invoiceStep === 3">

    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IInvoice, ILineItem } from "../types/Invoice";
import { ICustomer } from "../types/Customer";
import { IProductInventory } from "../types/Product";
import CustomerService from "../services/customer-service";
import { InventoryService } from "../services/inventory-service";
import InvoiceService from "../services/invoice-service";

const customerService = new CustomerService();
const inventoryService = new InventoryService();
const invoiceService = new InvoiceService();

@Component({ name: "CreateInvoice" })
export default class CreateInvoice extends Vue {
  invoiceStep = 1;
  invoice: IInvoice = {
    createdOn: new Date(),
    updatedOn: new Date(),
    customerId: 0,
    lineItems: []
  };
  customers: ICustomer[] = [];
  selectedCustomerId: number = 0;
  inventory: IProductInventory[] = [];
  lineItems: ILineItem[] = [];
  newItem: ILineItem = {product: undefined, quantity: 0};

  async initialize(): Promise<void> {
    // customerService.getCustomers().then(res => this.customers = res).catch(() => {});
    this.customers = await customerService.getCustomers();
    this.inventory = await inventoryService.getInvenotry();

  }
  async created() {
    await this.initialize();
  }
}
</script>

<style scoped lang="scss">

</style>