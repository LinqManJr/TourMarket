using Microsoft.EntityFrameworkCore;
using TourMarket.Models;

namespace TourMarket.Context
{
    public class TourRepository : MarketRepository<Tour>
    {
        public TourRepository(DbContext context) : base(context)
        {              
        }

        public bool IfExist(Tour tour) => FindById(tour.Id) != null;

    }
}
