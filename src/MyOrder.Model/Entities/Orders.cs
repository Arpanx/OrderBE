using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Model
{
    public class Orders : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        // public ICollection<Item> ItemsCreated { get; set; }
        public ICollection<Item> Items { get; set; }
        public Orders()
        {
            Items = new List<Item>();
        }
    }
}
