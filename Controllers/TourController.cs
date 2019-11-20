using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]    
    public class TourController : Controller
    {
        private readonly MarketOfTours repository;

        public TourController(MarketOfTours repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Tour> Get()
        {            
            return repository.Get();
        }

        [HttpGet("[action]/id")]
        public ActionResult<Tour> GetById(int id)
        {
            var tour = repository.FindById(id);
            if (tour == null)
                return NotFound();

            return Ok(tour);
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

            if (!repository.IfExist(tour))
                return BadRequest("Tour not exist");

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