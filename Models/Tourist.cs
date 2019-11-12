using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class Tourist
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<OrderTourist> OrderTourists{get;set;}
    }
}
