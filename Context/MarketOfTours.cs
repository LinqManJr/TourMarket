using Microsoft.EntityFrameworkCore;
using TourMarket.Models;

namespace TourMarket.Context
{
    public class MarketOfTours : MarketRepository<Tour>
    {
        public MarketOfTours(DbContext context) : base(context)
        {              
        }

        public bool IfExist(Tour tour) => FindById(tour.Id) != null;

    }
}
