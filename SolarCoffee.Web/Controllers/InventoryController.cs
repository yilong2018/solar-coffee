using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controller{
  [ApiController]
  public class InventoryController:ControllerBase{
    private ILogger<InventoryController> _logger;
    private IInventoryService _inventoryService;

    public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService){
      _logger = logger;
      _inventoryService = inventoryService;
    }

    [HttpGet("api/inventory")]
    public ActionResult GetCurrentInventory(){
      _logger.LogInformation("Getting all inventory ...");

      var inventory = _inventoryService.GetCurrentInventory()
                          .Select(pi => new ProductInventoryModel{
                                Id = pi.Id,
                                QuantityOnHand = pi.QuantityOnHand,
                                IdealQuantity = pi.IdealQuantity,
                                Product = ProductMapper.SerializeProduct(pi.Product)
                          })
                          .OrderBy(inv => inv.Product.Name)
                          .ToList();

      return Ok(inventory);
    }

    [HttpPatch("api/inventory")]
    public ActionResult UpdateIventory([FromBody] ShipmentModel shipment){
      if(!ModelState.IsValid){
        return BadRequest(ModelState);
      }
      var id = shipment.ProductId;
      var adjustment = shipment.Adjustment;
      _logger.LogInformation(
        "Update inventory" + 
        $"for {id} - " +
        $"Adjustment: {adjustment}");

      var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);
      return Ok(inventory);
    }
  }
}