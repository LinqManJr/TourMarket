using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Dto;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OrderController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Return Orders By Authorize Manager
        /// </summary>
        /// <returns>IEnumerable of Orders</returns>
        /// <response code="200">Returns Ok with orders data</response>
        /// <response code="404">If order count is 0</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IQueryable<Order>> GetOrdersByManager()
        {
            var managerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var orders = _repository.GetOrdersByManagerId(managerId).ToList();
            if (orders.Count == 0)
                return NotFound("You not have orders");

            return Ok(orders);
        }

        /// <summary>
        /// Return all orders in database
        /// </summary>
        /// <returns>IQueryable of Orders</returns>
        /// <response code="200">Returns Ok with orders data</response>
        /// <response code="404">If order count is 0</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IQueryable<Order>> GetOrders()
        {
            var orders = _repository.GetOrders();

            if (orders.Count() == 0)
                return NotFound("You not have orders");

            return Ok(orders);
        }

        /// <summary>
        /// Add new Order
        /// </summary>
        /// <param name="order">Order entity</param>
        /// <returns>ActionResult with Order data</returns>
        /// <response code="200">Returns Ok with order data</response>
        /// <response code="400">If ModelState is Valid</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> AddOrder([FromBody]Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            _repository.Create(order);
            return Ok(order);
        }

        /// <summary>
        /// Remove Order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>ActionResult (204 or 404)</returns>
        /// <response code="204">If Remove is true</response>
        /// <response code="404">If order is null</response>
        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Remove([FromBody]Order order)
        {
            if (!_repository.IfExist(order))
                return NotFound();

            _repository.Remove(order);
            return NoContent();
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>ActionResult with order data</returns>
        /// <response code="200">Returns Ok with order data</response>        
        /// <response code="404">If order is null</response>
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public ActionResult<Order> Update([FromBody]Order order)
        {
            if (!_repository.IfExist(order))
                return NotFound("Order not exist");

            _repository.Update(order);
            return Ok(order);
        }
    }
}