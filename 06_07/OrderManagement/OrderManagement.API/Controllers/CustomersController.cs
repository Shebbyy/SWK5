using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;
using OrderManagement.API.DTOs;
using OrderManagement.API.Mappers;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(WebApiConventions))]
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
                  (await _logic.GetCustomersAsync()).ToDTOEnumerable() 
                : (await _logic.GetCustomersByRatingAsync((Rating)rating)).ToDTOEnumerable();
        }
        
        // Alternatively at Route just Api and then here in HttpGet Parameter /customers
        // get /api/customers/cccc-..... for Customer with GUID 
        // REST by the book, Resource i no Longer all Customers, but specifically the Customer #1
        [HttpGet("{customerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById([FromRoute] Guid customerId) {
            var customer = await _logic.GetCustomerAsync(customerId);
            if (customer is null) {
                return NotFound(StatusInfo.InvalidCustomerId(customerId));
            }
            
            // To keep consistent, OK can be used; Only Works with ActionResult though
            return Ok(customer.ToDTO());
        }
        
        // Verbs in Routes absolute No-Go, always use the HTTP Actions, not like SOAP, REST Endpoints
        // CreateCustomer
        // POST /api/customers
        // Runtime would parse Post Request Body into CustomerDTO through magic
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerForUpsertDTO input) {
            Customer cust = input.ToDomain();
            await _logic.AddCustomerAsync(cust);
            
            // Created alternative, AtAction allows definition of route where the resource might be queried
            return CreatedAtAction(actionName: nameof(GetCustomerById), routeValues: new {customerId = cust.Id}, value: cust.ToDTO());
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> UpdateCustomer([FromRoute] Guid customerId,
            [FromBody] CustomerForUpsertDTO cust) {
            Customer? customer = await _logic.GetCustomerAsync(customerId);
            if (customer is null) {
                return NotFound(StatusInfo.InvalidCustomerId(customerId));
            }
            
            cust.UpdateCustomer(customer);
            await _logic.UpdateCustomerAsync(customer);
            
            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomer([FromRoute] Guid customerId) {
            if (await _logic.DeleteCustomerAsync(customerId)) {
                return NoContent();
            }

            return NotFound(StatusInfo.InvalidCustomerId(customerId));
        }

        [HttpPost("{customerId}/update-totals")]
        public async Task<ActionResult> UpdateCustomerTotals(Guid customerId) {
            if (!await _logic.CustomerExistsAsync(customerId)) {
                return NotFound(StatusInfo.InvalidCustomerId(customerId));
            }

            await _logic.UpdateTotalRevenueAsync(customerId);

            return NoContent();
        }
    }
}
