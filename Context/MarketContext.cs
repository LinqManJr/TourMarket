using Microsoft.EntityFrameworkCore;
using System;
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

            //seeding somedata
            builder.Entity<Tour>().HasData(
                new Tour { Id = 1, Name = "India", Price = 250 },
                new Tour { Id = 2, Name = "Thailand", Price = 300},
                new Tour { Id = 3, Name = "Japan", Price = 425},
                new Tour { Id = 4, Name = "China", Price = 350 });

            builder.Entity<Tourist>().HasData(
                new Tourist { Id = 1, Fio = "John Wick", Email = "jWick23@gmail.com", PhoneNumber = "655587442"},
                new Tourist { Id = 2, Fio = "Bruce Wayne", Email = "imbatman@gmail.com", PhoneNumber = "888888888" },
                new Tourist { Id = 3, Fio = "Peter Parker", Email = "spyda@yahoo.com", PhoneNumber = "615451442" },
                new Tourist { Id = 4, Fio = "Thony Start", Email = "ironman@gmail.com", PhoneNumber = "633331442" });

            builder.Entity<Order>().HasData(
                new Order { Id = 1, Date = DateTime.Now, TourId = 1 },
                new Order { Id = 2, Date = DateTime.Now.AddDays(-1), TourId = 2 },
                new Order { Id = 3, Date = DateTime.Now.AddDays(-2), TourId = 3 },
                new Order { Id = 4, Date = DateTime.Now.AddDays(-3), TourId = 4 }
                );

            builder.Entity<OrderManager>().HasData(
                new OrderManager { OrderId = 1, ManagerId = 1},
                new OrderManager { OrderId = 2, ManagerId = 1},
                new OrderManager { OrderId = 3, ManagerId = 1},
                new OrderManager { OrderId = 4, ManagerId = 1}
                );

            builder.Entity<OrderTourist>().HasData(
                new OrderTourist { OrderId = 1, TouristId = 1},
                new OrderTourist { OrderId = 2, TouristId = 2 },
                new OrderTourist { OrderId = 3, TouristId = 3 },
                new OrderTourist { OrderId = 4, TouristId = 4 }
                );
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<OrderManager> OrderManagers { get; set; }
        public DbSet<OrderTourist> OrderTourist { get; set; }
    }
}
