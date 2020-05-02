using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;

namespace SolarCoffee.Services.Customer{
  public class CustomerService : ICustomerService
  {

    private readonly SolarDbContext _db;
    public CustomerService(SolarDbContext dbContext){
      _db = dbContext;
    }

    //Return a list of customers from database
    public List<Data.Models.Customer> GetAllCustomers()
    {
      //return _db.Customers.ToList();
      return _db.Customers
        .Include(customer => customer.PrimaryAddress)
        .OrderBy(customer => customer.LastName)
        .ToList();
    }


    //Add a new customer
    public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
    {
      try{
        _db.Add(customer);
        _db.SaveChanges();
        return new ServiceResponse<Data.Models.Customer>{
          IsSuccess = true,
          Message = "New Customer Add",
          Time = DateTime.UtcNow,
          Data = customer
        };
      }catch(Exception e){
        return new ServiceResponse<Data.Models.Customer>{
          IsSuccess = false,
          Message = e.StackTrace,
          Time = DateTime.UtcNow,
          Data = customer
        };
      }
    }

    //Delete a customer record
    public ServiceResponse<bool> DeleteCustomer(int id)
    {
      var customer = _db.Customers.Find(id);
      var now = DateTime.UtcNow;
      if(customer == null){
        return new ServiceResponse<bool>{
          IsSuccess = false,
          Message = "Customer to delete not found.",
          Time = now,
          Data = false
        };
      }

      try{
        _db.Customers.Remove(customer);
        _db.SaveChanges();

        return new ServiceResponse<bool>{
          IsSuccess = true,
          Message = "Customer deleted!",
          Time = now,
          Data = true
        };
      }catch(Exception e){
        return new ServiceResponse<bool>{
          IsSuccess = false,
          Message = e.StackTrace,
          Time = now,
          Data = false
        };
      }
    }

    //Get a customer record by primary key
    public Data.Models.Customer GetById(int id)
    {
      //return _db.Customers.FirstOrDefault(c => c.Id == id);
      //the same as above
      return _db.Customers.Find(id);

    }
  }
}