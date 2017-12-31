﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Model
{
    public class Orders : IEntityBase
    {
        public Orders()
        {
            ItemsCreated = new List<Item>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public ICollection<Item> ItemsCreated { get; set; }        
    }
}
