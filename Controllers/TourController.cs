using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]    
    public class TourController : Controller
    {
        private readonly MarketRepository<Tour> repository;

        public TourController(MarketRepository<Tour> repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Tour> Get()
        {            
            return repository.Get();
        }

        [HttpGet("[action]/id")]
        public Tour GetById(int id)
        {
            return repository.FindById(id);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Tour tour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnTour = repository.Create(tour);
            return Ok(returnTour);
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody] Tour tour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //TODO: check if tour exist (need to repository of tour, where we check existance)
            repository.Update(tour);
            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult Remove(Tour tour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            repository.Remove(tour);
            return Ok();
        }
    }
}