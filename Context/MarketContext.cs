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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Order>().HasKey(t => t.Id);
            builder.Entity<Tourist>().HasKey(t => t.Id);

            builder.Entity<OrderManager>()
                .HasKey(t => new { t.OrderId, t.ManagerId });

            builder.Entity<OrderManager>()
                .HasOne(t => t.Manager)
                .WithMany(m => m.OrderManagers)
                .HasForeignKey(t => t.ManagerId);
            builder.Entity<OrderManager>()
                .HasOne(t => t.Order)
                .WithMany(m => m.OrderManagers)
                .HasForeignKey(t => t.OrderId);

            builder.Entity<OrderTourist>()
                .HasKey(t => new { t.OrderId, t.TouristId});
            builder.Entity<OrderTourist>()
                .HasOne(t => t.Order)
                .WithMany(m => m.OrderTourists)
                .HasForeignKey(t => t.OrderId);
            builder.Entity<OrderTourist>()
                .HasOne(t => t.Tourist)
                .WithMany(m => m.OrderTourists)
                .HasForeignKey(t => t.TouristId);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}
