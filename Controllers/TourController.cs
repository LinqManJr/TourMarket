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
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly MarketRepository<Tour> repository;

        public TourController(MarketRepository<Tour> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Tour> Get()
        {
            return repository.Get();
        }

        [HttpPost]

        public IActionResult Create(Tour tour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnTour = repository.Create(tour);
            return Ok(returnTour);
        }
    }
}