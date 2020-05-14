using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory{
  public class InventoryService : IInventoryService
  {
    private readonly SolarDbContext _db;
    private readonly ILogger<InventoryService> _logger;
    
    public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger){
      _db = dbContext;
      _logger = logger;
    }

        //Get Inventory by Id
    public ProductInventory GetByProductId(int productId)
    {
      return _db.ProductInventorys
              .Include(pi => pi.Product)
              .FirstOrDefault(pi => pi.Product.Id == productId);
    }

    //Return Snapshot history for the previous 6 hours
    public List<ProductInventorySnapshot> GetSnapshotHistory()
    {
      var earliest = DateTime.UtcNow - TimeSpan.FromHours(266);
      return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap 
                    => snap.SnapshotTime > earliest
                        && !snap.Product.IsArchived)
                .ToList();
    }

    //Return all current inventory from database
    public List<ProductInventory> GetCurrentInventory()
    {
      return _db.ProductInventorys
        .Include(pi => pi.Product)
        .Where(pi => !pi.Product.IsArchived)
        .ToList();
    }

    //Upadte number of units available of the productId
    //Adjust QuantityOnHand by adjustment value
    public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
    {
      var now = DateTime.UtcNow;
      try{
        var inventory = _db.ProductInventorys
                            .Include(inv => inv.Product)
                            .First(inv => inv.Product.Id == id);
        try{
          CreateSnapshot(inventory);
        }catch(Exception e){
          _logger.LogError("Error Creating Inventory snapshot");
          _logger.LogError(e.StackTrace);
        }
        inventory.QuantityOnHand += adjustment;

        _db.SaveChanges();
        return new ServiceResponse<ProductInventory>{
            IsSuccess = true,
            Data = inventory,
            Message = $"Product {id} inventory adjustment",
            Time = now
        };
      }catch(Exception e){
        return new ServiceResponse<ProductInventory>{
            IsSuccess = false,
            Data = null,
            //Message = "Error Updating Product QuantityOnHand",
            Message = e.StackTrace,
            Time = now
        };
      }
    }

    //Create a snapshot record using the provided ProductIventory instance
    private void CreateSnapshot(ProductInventory inventory)
    {
      var now = DateTime.UtcNow;
      var snapshot = new ProductInventorySnapshot{
        SnapshotTime = now,
        Product = inventory.Product,
        QuantityOnHand = inventory.QuantityOnHand
      };
      _db.ProductInventorySnapshots.Add(snapshot);
    }

  }
}