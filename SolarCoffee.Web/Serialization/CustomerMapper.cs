using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization{
  public static class CustomerMapper{
    //Serialize a customer data model into a customer view model
    public static CustomerModel SerializeCustomer(Customer customer){

      return new CustomerModel{
        Id = customer.Id,
        CreatedOn = customer.CreatedOn,
        UpdatedOn = customer.UpdatedOn,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
      };
    }
  
    //Serialize a customer view model into a customer data model
    public static Customer SerializeCustomerModel(CustomerModel customer){
      
      return new Customer{
        CreatedOn = customer.CreatedOn,
        UpdatedOn = customer.UpdatedOn,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
      };
    }


    //Map a CustomerAddress data model into a CustomerAddressModel view model
    public static CustomerAddressModel MapCustomerAddress (CustomerAddress address)
    {
      return new CustomerAddressModel{
        Id = address.Id,
        AddressLine1 = address.AddressLine1,
        AddressLine2 = address.AddressLine2,
        City = address.City,
        State = address.State,
        Country = address.Country,
        PostalCode = address.PostalCode,
        CreatedOn = address.CreatedOn,
        UpdatedOn = address.UpdatedOn
      };
    }

    //Map a CustomerAddressModel view model into a CustomerAddress data model
    public static CustomerAddress MapCustomerAddress (CustomerAddressModel address)
    {
      return new CustomerAddress{
        Id = address.Id,
        AddressLine1 = address.AddressLine1,
        AddressLine2 = address.AddressLine2,
        City = address.City,
        State = address.State,
        Country = address.Country,
        PostalCode = address.PostalCode,
        CreatedOn = address.CreatedOn,
        UpdatedOn = address.UpdatedOn
      };
    }

  } 
}