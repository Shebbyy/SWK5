using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs;
using OrderManagement.API.Mappers;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(
        IOrderManagementLogic logic) : ControllerBase
    {
        private readonly IOrderManagementLogic _logic = logic ?? throw new ArgumentNullException(nameof(logic));
        
        // Alternatively at Route just Api and then here in HttpGet Parameter /customers
        // get /api/customers?rating=A
        // Specify if from Query or from Path in Parameter
        [HttpGet]
        public async Task<IEnumerable<CustomerDTO>> GetCustomers([FromQuery] Rating? rating) {
            return rating is null ? 
                  (await _logic.GetCustomersAsync()).Select(c => c.ToDTO()) 
                : (await _logic.GetCustomersByRatingAsync((Rating)rating)).Select(c => c.ToDTO());
        }
        
        // Alternatively at Route just Api and then here in HttpGet Parameter /customers
        // get /api/customers/cccc-..... for Customer with GUID 
        // REST by the book, Resource i no Longer all Customers, but specifically the Customer #1
        [HttpGet("{customerId:guid}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById([FromRoute] Guid customerId) {
            var customer = await _logic.GetCustomerAsync(customerId);
            if (customer is null) {
                return NotFound();
            }
            
            // To keep consistent, OK can be used; Only Works with ActionResult though
            return Ok(customer.ToDTO());
        }
        
        // Verbs in Routes absolute No-Go, always use the HTTP Actions, not like SOAP, REST Endpoints
    }
}
