using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyOrder.Model;
using MyOrder.Data;
using MyOrder.Data.Repositories;
using MyOrder.Data.Abstract;

namespace MyOrder.Data.Repositories
{
    public class OrderRepository : EntityBaseRepository<Orders>, IOrderRepository
    {
        public OrderRepository(OrderContext context)
            : base(context)
        { }
    }
}
