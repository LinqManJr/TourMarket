using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Context;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TouristController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly TouristsRepository repository;

        public TouristController(TouristsRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Return Tourists
        /// </summary>
        /// <returns>IEnumerable of Tourists</returns>
        [HttpGet("[action]")]
        public IEnumerable<Tourist> Get()
        {
            return repository.Get();
        }

        /// <summary>
        /// Return tourist by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Tourist by specific id</returns>
        /// <response code="200">Returns Ok with tourist data</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("[action]/id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Tourist> GetById(int id)
        {
            var tourist = repository.FindById(id);
            if (tourist == null)
                return NotFound();
            
            return Ok(tourist);
        }

        /// <summary>
        /// Add new Tourist
        /// </summary>
        /// <param name="tourist">Tourist</param>
        /// <returns>ActionResult with Tourist</returns>
        /// <response code="200">Returns Ok with tourist data</response>
        /// <response code="400">If ModelState is Valid</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Tourist> Create([FromBody]Tourist tourist)
        {       
            if (string.IsNullOrWhiteSpace(tourist.Fio))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTourist = repository.Create(tourist);
            return Ok(newTourist);
        }

        /// <summary>
        /// Update Tourist data
        /// </summary>
        /// <param name="tourist">Tourist</param>
        /// <returns>ActionResult with Tourist</returns>
        /// <response code="200">Returns Ok with tourist data</response>
        /// <response code="400">If ModelState is Valid</response>
        /// <response code="404">If tourist is null</response>
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Tourist> Update([FromBody] Tourist tourist)
        {
            if (string.IsNullOrWhiteSpace(tourist.Fio))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!repository.IfExist(tourist))
                return NotFound("Tourist not exist");

            repository.Update(tourist);
            return Ok(tourist);
        }

        /// <summary>
        /// Remove Tourist
        /// </summary>
        /// <param name="tourist">Tourist</param>
        /// <returns>ActionResult (204 or 404)</returns>
        /// <response code="204">If Remove is true</response>
        /// <response code="404">If tourist is null</response>
        [HttpDelete("[action]")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Remove(Tourist tourist)
        {
            if (!repository.IfExist(tourist))
                return NotFound();

            repository.Remove(tourist);
            return NoContent();
        }
    }
}