using System;
using System.Collections.Generic;

namespace MyOrder.API.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Order { get; set; }
        public int OrderId { get; set; }
 
        // Lookups
        public string[] Statuses { get; set; }
        public string[] Types { get; set; }
    }
}
