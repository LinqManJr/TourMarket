using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Models;

namespace TourMarket.Context
{
    public class TouristsRepository : MarketRepository<Tourist>
    {
        public TouristsRepository(DbContext context) : base(context)
        {
        }

        public bool IfExist(Tourist tourist)
        {
            return _dbSet.Any(x => x.PhoneNumber == tourist.PhoneNumber);
        }
    }
}
