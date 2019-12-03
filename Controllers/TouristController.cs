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
        public ActionResult<Tourist> GetById(int id)
        {
            var tourist = repository.FindById(id);
            if (tourist == null)
                return NotFound();
            
            return tourist;
        }

        [HttpPost("[action]")]
        public ActionResult<Tourist> Create([FromBody]Tourist tourist)
        {       
            if (string.IsNullOrWhiteSpace(tourist.Fio))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTourist = repository.Create(tourist);
            return newTourist;
        }

        [HttpPut("[action]")]
        public ActionResult<Tourist> Update([FromBody] Tourist tourist)
        {
            if (string.IsNullOrWhiteSpace(tourist.Fio))
                ModelState.AddModelError("Name", "Tour name must be non empty string");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!repository.IfExist(tourist))
                return NotFound("Tourist not exist");

            repository.UpdateAsync(tourist);
            return tourist;
        }

        [HttpDelete("[action]")]
        public ActionResult Remove(Tourist tourist)
        {
            if (!repository.IfExist(tourist))
                return NotFound();

            repository.Remove(tourist);
            return NoContent();
        }
    }
}