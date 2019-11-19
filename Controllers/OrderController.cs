using System;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Dto;

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

        [HttpGet("[action]")]        
        public IActionResult GetOrders(int id = 1)
        {
            var orders = _repository.GetOrdersByManagerId(id);
            
            //orders to ordersdto
            return Ok(orders);
        }

        [HttpPost("[action]")]
        public IActionResult AddOrder([FromBody]OrderDto order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //TODO: add manager id to Order 
            _repository.AddOrder(order);
            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody]OrderController dto)
        {
            throw new NotImplementedException();
        }
    }
}