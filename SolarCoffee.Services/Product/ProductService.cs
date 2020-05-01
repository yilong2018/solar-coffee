using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Product {
  public class ProductService : IProductService
  {
    private readonly SolarDbContext _db;
    public ProductService(SolarDbContext dbContext){
      _db = dbContext;
    }
    //Retrive All Products from Database
    public List<Data.Models.Product> GetAllProducts(){
      return _db.Products.ToList();
    }

    //Retrive a Products By Primary Key
    public Data.Models.Product GetProductById(int id){
      return _db.Products.Find(id);
    }

    //Adds a new product to the database
    public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product){
      try{
        _db.Products.Add(product);
        ProductInventory newInventory = new ProductInventory(){
          Product = product,
          QuantityOnHand = 0,
          IdealQuantity = 10
        };
        _db.ProductInventorys.Add(newInventory);
        _db.SaveChanges();

        return new ServiceResponse<Data.Models.Product>{
          Data = product,
          IsSuccess = true,
          Time = DateTime.UtcNow,
          Message = "Saved new products"
        };
      }catch(Exception e)
      {
        return new ServiceResponse<Data.Models.Product>{
          Data = product,
          IsSuccess = false,
          Time = DateTime.UtcNow,
          Message = e.StackTrace
        };
      }
    }

    //Archive a product by setting boolean IsArchived to true
    public ServiceResponse<Data.Models.Product> ArchiveProduct(int id){
      try{
        var product = _db.Products.Find(id);
        product.IsArchived = true;
        _db.SaveChanges();

        return new ServiceResponse<Data.Models.Product>{
          Data = product,
          IsSuccess = false,
          Time = DateTime.UtcNow,
          Message = "Archived Product"
        };

      }catch(Exception e){
          return new ServiceResponse<Data.Models.Product>{
          Data = null,
          IsSuccess = false,
          Time = DateTime.UtcNow,
          Message = e.StackTrace
        };
      }
    }

  }
}