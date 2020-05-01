using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.Controller
{
  [ApiController]
  public class ProductController: ControllerBase{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    public ProductController(ILogger<ProductController> logger, IProductService productService){
      _logger = logger;
      _productService = productService;
    }

    [HttpGet("api/product")]
    public ActionResult GetProduct(){
      _logger.LogInformation("Get All Products");
      var products = _productService.GetAllProducts();
      //var productViewmodels = products.Select(product => ProductMapper.SerializeProductModel(product));
      //上面的语句可以简写：
      var productViewmodels = products
      .Select(ProductMapper.SerializeProductModel);
      return Ok(productViewmodels);
    }
  }
}