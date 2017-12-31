using MyOrder.Data.Abstract;
using MyOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Data.Repositories
{
    public class ItemRepository : EntityBaseRepository<Item>, IItemRepository
    {
        public ItemRepository(OrderContext context)
            : base(context)
        { }
    }
}
