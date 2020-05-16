using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

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

    // Add a new product
    [HttpPost("api/product")]
    public ActionResult AddProduct([FromBody] ProductModel product){
      if(!ModelState.IsValid){
        return BadRequest(ModelState); //400 respond
      }
      _logger.LogInformation("Adding product");
      var newProduct = ProductMapper.SerializeProductModel(product);
      var newProductResponse = _productService.CreateProduct(newProduct);
      return Ok(newProductResponse);
    }

    // Returns All products
    [HttpGet("api/product")]
    public ActionResult GetProduct(){
      _logger.LogInformation("Get All Products");
      var products = _productService.GetAllProducts();
      //var productViewmodels = products.Select(product => ProductMapper.SerializeProductModel(product));
      //上面的语句可以简写：
      var productViewmodels = products
      .Select(ProductMapper.SerializeProduct);
      return Ok(productViewmodels);
    }

    // Archive an existing product
    [HttpPatch("api/product/{id}")]
    public ActionResult ArchiveProduct(int id){
      _logger.LogInformation("Archive product");
      var archiveResult = _productService.ArchiveProduct(id);
      return Ok(archiveResult);
    }
  }
}