using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Model
{
    public class Item : IEntityBase
    {        
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public OrderType Type { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        
        public int? OrderId { get; set; }
        public Orders Order { get; set; }
    }
}
