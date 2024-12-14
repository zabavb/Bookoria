﻿using Microsoft.AspNetCore.Mvc;
using OrderApi.Services;

namespace OrderApi.Controllers
{
    /// <summary>
    /// Manage order-related operations
    /// </summary>
    /// <remarks>
    /// This controller provides CRUD operations for Orders
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retrieves list of orders
        /// </summary>
        /// <returns>List of orders</returns>
        /// <response code="200">Retrieval successful, returns the list</response>
        /// <response code="404">Could not find the orders</response>
        [HttpGet]
        [Route("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found");
            }

            return Ok(orders);
        }

        /// <summary>
        /// Retrieves Order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Order which id matches with given one</returns>
        /// <response code="200">Retrieval successful, return the order</response>
        /// <response cose="404">Could not find the order</response>
        [HttpGet("{id}")]

        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if(order == null)
            {
                return NotFound($"Book with Id:{id} not found.");
            }
            return Ok(order);
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="orderDto">Order data</param>
        /// <returns>Created order</returns>
        /// <response code="201">Order created successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody]OrderDto orderDto)
        {
            if (orderDto == null || !ModelState.IsValid)
            {
                return BadRequest("InvalidData"); 
            }

            var newOrder = await _orderService.CreateOrderAsync(orderDto);

            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
        }

        /// <summary>
        /// Updates existing order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="orderDto">Updated order data</param>
        /// <returns>The updated order</returns>
        /// <response code="200">Order updated successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(Guid id, [FromBody]OrderDto orderDto)
        {
            if (orderDto == null || !ModelState.IsValid)
            {
                return BadRequest("InvalidData.");
            }



            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderDto);

            return Ok(updatedOrder);
        }

        /// <summary>
        /// Deletes a order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>NoContent on success</returns>
        /// <response code="204">Order deleted successfully</response>
        /// <response code="404">Could not find the order</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var isDeleted = await _orderService.DeleteOrderAsync(id);

            if (!isDeleted)
            {
                return NotFound("Order not found.");
            }

            return NoContent();
        }
    }
}
