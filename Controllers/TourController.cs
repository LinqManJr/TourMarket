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

            return tour;
        }

        [HttpPost("[action]")]
        public ActionResult<Tour> Create([FromBody]Tour tour)
        {
            if (tour.Price == null | tour.Price == 0)
                ModelState.AddModelError("Price", "Price must be positive number");

            if (string.IsNullOrWhiteSpace(tour.Name))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnTour = repository.Create(tour);
            return returnTour;
            //return Created("", returnTour);
        }

        [HttpPut("[action]")]
        public ActionResult Update([FromBody] Tour tour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!repository.IfExist(tour))
                return BadRequest("Tour not exist");

            repository.Update(tour);
            return Ok();
        }

        [HttpDelete("[action]")]
        public ActionResult Remove(Tour tour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            repository.Remove(tour);
            return NoContent();
        }
    }
}