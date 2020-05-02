using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order{
  public class OrderService : IOrderService
  {
    private readonly SolarDbContext _db;
    private readonly ILogger<OrderService> _logger;
    private readonly IProductService _productService;
    private readonly IInventoryService _inventoryService;


    public OrderService(
      SolarDbContext dbContext,
       ILogger<OrderService> logger,
       IProductService productService,
       IInventoryService inventoryService)
       {
      _db = dbContext;
      _logger = logger;
      _productService = productService;
      _inventoryService = inventoryService;
    }


    //Creates an open SalesOrder
    public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
    {
      var now = DateTime.UtcNow;
      _logger.LogInformation("Generating new order");
      foreach(var item in order.SalesOrderItems){
        item.Product = _productService.GetProductById(item.Product.Id);
        var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
        _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
      }
      try{
        _db.Add(order);
        _db.SaveChanges();
        return new ServiceResponse<bool>{
          IsSuccess = true,
          Data = true,
          Time = now,
          Message = "Open Order Created"
          };
        }
        catch(Exception e){
        return new ServiceResponse<bool>{
          IsSuccess = false,
          Data = false,
          Time = now,
          Message = e.StackTrace
          };
      }
    }

    //Get all SalesOrders from database
    public List<SalesOrder> GetOrders()
    {
      return _db.SalesOrders
              .Include(order => order.Customer)
                .ThenInclude(customer => customer.PrimaryAddress)
              .Include(order => order.SalesOrderItems)
                .ThenInclude(item => item.Product)
              .ToList();
    }

    //Makrs an open Order as paid
    public ServiceResponse<bool> MarkFullFilled(int id)
    {
      var now = DateTime.UtcNow;
      var order = _db.SalesOrders.Find(id);
      order.UpdatedOn = now;
      order.IsPaid = true;
      try{
        _db.SalesOrders.Update(order);
        _db.SaveChanges();
        return new ServiceResponse<bool>{
          IsSuccess = true,
          Data = true,
          Time = now,
          Message = $"Order {order.Id} Closed: Invoice paid in full"
        };
      }catch(Exception e){
        return new ServiceResponse<bool>{
          IsSuccess = false,
          Data = false,
          Time = now,
          Message = e.StackTrace
        };
      }
    }
  }
}