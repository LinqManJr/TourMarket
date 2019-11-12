using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class OrderTourist
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int TouristId { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}
