using System;
using System.Linq;
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
                return NotFound();

            return Ok(orders);
        }

        [HttpPost("[action]")]
        public IActionResult AddOrder([FromBody]Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //TODO: add manager id to Order 
            _repository.AddOrderTest(order);
            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody]Order order)
        {
            throw new NotImplementedException();
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody]Order order)
        {
            throw new NotImplementedException();
        }
    }
}