using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Models;

namespace TourMarket.Context
{
    public class MarketContext : DbContext
    {

        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}
