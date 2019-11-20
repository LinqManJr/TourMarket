using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public decimal? Price { get; set; }

        public virtual ICollection<Order> Orders{get;set;}
    }
}
