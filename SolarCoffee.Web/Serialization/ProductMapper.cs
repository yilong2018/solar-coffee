using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
  public class ProductMapper{
    //Map a Product data model to a ProductModel View model
    public static ProductModel SerializeProductModel(Data.Models.Product product){
      return new ProductModel{
        Id = product.Id,
        CreatedOn = product.CreatedOn,
        UpdatedOn = product.UpdatedOn,
        Price = product.Price,
        Name = product.Name,
        Description = product.Description,
        IsArchived = product.IsArchived,
        IsTaxable = product.IsTaxable
      };
    } 

    //Map a ProductModel View model to a Product data model
    public static Data.Models.Product SerializeProduct(ProductModel product){
      return new Data.Models.Product{
        Id = product.Id,
        CreatedOn = product.CreatedOn,
        UpdatedOn = product.UpdatedOn,
        Price = product.Price,
        Name = product.Name,
        Description = product.Description,
        IsArchived = product.IsArchived,
        IsTaxable = product.IsTaxable
      };
    } 
  }
}