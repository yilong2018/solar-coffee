using System;
using System.Collections.Generic;

namespace SolarCoffee.Web.ViewModels{

  //ViewModel for open salesOrders
  public class InvoiceModel{
    public int Id{get;set;}
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int CustomerId { get; set; }
    public List<SalesOrderItemModel> LineItems { get; set; }
  }

  //ViewModel for SalesOrderItems
  public class SalesOrderItemModel{
    public int Id{get;set;}
    public int Quantity { get; set; }
    public ProductModel Product { get; set; }
  }
}