using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]    
    public class TouristController : Controller
    {
        private readonly TouristsRepository repository;

        public TouristController(TouristsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Tourist> Get()
        {
            return repository.Get();
        }

        [HttpGet("[action]/id")]
        public Tourist GetById(int id)
        {
            return repository.FindById(id);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Tourist tourist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnTour = repository.Create(tourist);
            return Ok(returnTour);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody] Tourist tourist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!repository.IfExist(tourist))
                return BadRequest("Tourist not exist");

            repository.Update(tourist);
            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult Remove(Tourist tourist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            repository.Remove(tourist);
            return Ok();
        }
    }
}