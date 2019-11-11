using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Order> Orders{get;set;}
    }
}
