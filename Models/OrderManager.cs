using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class OrderManager
    {
        [Key]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Key]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
