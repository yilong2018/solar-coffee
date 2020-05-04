using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controller{
  public class CustomerController:ControllerBase{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService){
      _logger = logger;
      _customerService =customerService;
    }

    [HttpPost("api/customer")]
    public ActionResult CreateCustomer([FromBody] CustomerModel customer){
      var now = DateTime.UtcNow;
      _logger.LogInformation("Create a customer");
      customer.CreatedOn = now;
      customer.UpdatedOn = now;
      var customerData = CustomerMapper.SerializeCustomerModel(customer);
      var newCustomer = _customerService.CreateCustomer(customerData);
      return Ok(newCustomer);
    }

    [HttpGet("api/customer")]
    public ActionResult GetCustomer(){
      _logger.LogInformation("Getting customers");
      var customers = _customerService.GetAllCustomers();
      var customerModels = customers.
                              Select(customer => 
                              CustomerMapper.SerializeCustomer(customer)).OrderByDescending(customer => customer.CreatedOn)
                              .ToList();
      return Ok(customerModels);  //Ok == 200 response
    }

    [HttpDelete("api/customer/{id}")]
    public ActionResult DeleteCustomer(int id){
      _logger.LogInformation("Deleting a customer");
      var response = _customerService.DeleteCustomer(id);
      return Ok(response);
    }
  }
}