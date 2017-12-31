using MyOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Data.Abstract
{
    public interface IItemRepository : IEntityBaseRepository<Item> { }

    public interface IOrderRepository : IEntityBaseRepository<Orders> { }
    
}
