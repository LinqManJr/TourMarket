using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get;set; }
        public virtual ICollection<OrderManager> OrderManagers { get; set; }
        public virtual ICollection<OrderTourist> OrderTourists{ get; set; }
    }
}
