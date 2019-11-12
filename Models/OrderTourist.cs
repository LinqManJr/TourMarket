using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class OrderTourist
    {
        [Key]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Key]
        public int TouristId { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}
