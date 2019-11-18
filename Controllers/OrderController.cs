using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            //TODO: add authorization
            return Ok(_repository.GetOrdersByManagerId(id));
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
    }
}