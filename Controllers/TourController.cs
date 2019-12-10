using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TourController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly TourRepository repository;

        public TourController(TourRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Return collection of tours
        /// </summary>        
        /// <returns>IEnumerable of tours</returns>        
        [HttpGet("[action]")]
        public IEnumerable<Tour> GetTours()
        {            
            return repository.Get();
        }
        /// <summary>
        /// Return tour by specific id
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>Tour entity</returns>
        /// <response code="200">Returns Ok with tour data</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("[action]/id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Tour> GetById(int id)
        {
            var tour = repository.FindById(id);
            if (tour == null)
                return NotFound();

            return Ok(tour);
        }

        /// <summary>
        /// Create Tour
        /// </summary>
        /// <param name="tour">Tour</param>
        /// <returns>ActionResult with tour</returns>
        /// <response code="200">Returns Ok with tour data</response>
        /// <response code="400">If ModelState is Valid</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Tour> Create([FromBody]Tour tour)
        {
            if (tour.Price == null | tour.Price == 0)
                ModelState.AddModelError("Price", "Price must be positive number");

            if (string.IsNullOrWhiteSpace(tour.Name))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnTour = repository.Create(tour);
            return Ok(returnTour);           
        }

        /// <summary>
        /// Update tour
        /// </summary>
        /// <param name="tour">Tour</param>
        /// <returns>ActionResult with Tour</returns>
        /// <response code="200">Returns Ok with tour data</response>
        /// <response code="400">If ModelState is Valid</response>
        /// <response code="404">If tour is null</response>
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult<Tour> Update([FromBody] Tour tour)
        {
            if (tour.Price == null | tour.Price == 0)
                ModelState.AddModelError("Price", "Price must be positive number");

            if (string.IsNullOrWhiteSpace(tour.Name))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!repository.IfExist(tour))
                return NotFound("Tour not exist");

            repository.Update(tour);
            return Ok(tour);
        }

        /// <summary>
        /// Remove Tour
        /// </summary>
        /// <param name="tour">Tour</param>
        /// <returns>ActionResult (204 or 404)</returns>        
        /// <response code="204">If Remove is true</response>
        /// <response code="404">If tour is null</response>
        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Remove(Tour tour)
        {
            if (!repository.IfExist(tour))
                return NotFound();

            repository.Remove(tour);
            return NoContent();
        }
    }
}