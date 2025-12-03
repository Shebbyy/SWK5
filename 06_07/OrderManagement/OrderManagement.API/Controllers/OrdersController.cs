using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs;
using OrderManagement.API.Mappers;
using OrderManagement.Logic;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController (IOrderManagementLogic orderManagementLogic) : ControllerBase {
        private readonly IOrderManagementLogic _orderManagementLogic = orderManagementLogic 
            ?? throw new ArgumentNullException(nameof(orderManagementLogic));
        // /api/customer/CUSTOMER_GUI/orders
        // how to achieve, although above api/[controller]:
        // as soon as HttpGet Param starts with / all prior route info gets lost
        // alternatively change Route above to just api and work from there
        [HttpGet("/api/customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersOfCustomer(Guid customerId) {
            if (!await _orderManagementLogic.CustomerExistsAsync(customerId)) {
                return NotFound();
            }

            var orderApiCustomer = await _orderManagementLogic.GetOrdersOfCustomerAsync(customerId);

            return Ok(orderApiCustomer.Select(o => o.ToOrderDTO()));
        }
    }
}
