using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Dto;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]    
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("[action]/{id}")]        
        public ActionResult<IQueryable<Order>> GetOrders(int id = 1)
        {
            var orders = _repository.GetOrdersByManagerId(id).ToList();
            if (orders.Count == 0)
                return NotFound("You not have orders");

            return Ok(orders);
        }

        [HttpPost("[action]")]
        public ActionResult<Order> AddOrder([FromBody]Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            _repository.Create(order);
            return order;
        }

        [HttpDelete("[action]")]
        public ActionResult Remove([FromBody]Order order)
        {
            if (!_repository.IfExist(order))
                return NotFound();

            _repository.Remove(order);
            return NoContent();
        }

        [HttpPut("[action]")]
        public ActionResult<Order> Update([FromBody]Order order)
        {
            if (!_repository.IfExist(order))
                return NotFound("Order not exist");

            _repository.Update(order);
            return order;
        }
    }
}