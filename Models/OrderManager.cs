using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class OrderManager
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
