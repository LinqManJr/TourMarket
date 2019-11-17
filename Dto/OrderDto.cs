using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TourName { get; set; }
        public double TourPrice { get; set; }
        public string ManagerName { get; set; }
        public string TouristName { get; set; }
    }
}
