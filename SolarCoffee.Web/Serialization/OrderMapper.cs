using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
  //Handles mapping Order data models from related View Models
  public static class OrderMapper{
    //Map a Product View Model to a Product data model
    public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice){
      var now = DateTime.UtcNow;
      var salesOrderItems = invoice.LineItems
                              .Select(item => new SalesOrderItem{
                                Id = item.Id,
                                Quantity = item.Quantity,
                                Product = ProductMapper.SerializeProductModel(item.Product)
                              }).ToList();
      return new SalesOrder{
        SalesOrderItems = salesOrderItems,
        CreatedOn = now,
        UpdatedOn = now
      };
    } 

    //Map a SalesOrders (data) to OrderModels (View Models)
    public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders){
      return orders.Select(order => new OrderModel{
        Id = order.Id,
        CreatedOn = order.CreatedOn,
        UpdatedOn = order.UpdatedOn,
        SalesOrderItems = SerializeOrderItems(order.SalesOrderItems),
        Customer = CustomerMapper.SerializeCustomer(order.Customer),
        IsPaid = order.IsPaid
      }).ToList();

      
    }

    //Map a collection of SalesOrderItem (data) to SalesOrderItemModels (View Model)
    private static List<SalesOrderItemModel> SerializeOrderItems(IEnumerable<SalesOrderItem> orderItems){
      return orderItems.Select(item => new SalesOrderItemModel{
                                          Id = item.Id,
                                          Quantity = item.Quantity,
                                          Product = ProductMapper.SerializeProduct(item.Product)
                                        })
                        .ToList();
    }
  }
}