using System.Collections.Generic;

namespace TourMarket.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }        
        public virtual ICollection<OrderManager> OrderManagers { get; set; }
    }
}
