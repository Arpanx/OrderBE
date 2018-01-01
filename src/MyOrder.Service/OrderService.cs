using MyOrder.Data.Abstract;
using MyOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyOrder.Service
{
    public interface IOrderService
    {
        IEnumerable<Orders> Get(int currentPage, int currentPageSize);
        Orders Get(int id);
        void Create(Orders order);
        Orders Put(int id, Orders order);
        int Delete(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;

        public OrderService(IOrderRepository orderRepository) 
        {
            this._orderRepository = orderRepository;
        }

        #region IOrderService Members

        public IEnumerable<Orders> Get(int currentPage, int currentPageSize)
        {
            IEnumerable<Orders> _orders = _orderRepository
                .AllIncluding(u => u.ItemsCreated)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            return _orders;
        }

        public Orders Get(int id)
        {
            Orders _order = _orderRepository.GetSingle(u => u.Id == id, u => u.ItemsCreated);

            return _order;
        }

        public void Create(Orders order)
        {
            Orders _newOrder = new Orders { Name = order.Name, Address = order.Address, City = order.City };
            _orderRepository.Add(_newOrder);
            _orderRepository.Commit();
        }

        public Orders Put(int id, Orders order)
        {          
            Orders _orderDb = _orderRepository.GetSingle(id);

            if (_orderDb == null)
            {
                return new Orders();
            }
            else
            {
                _orderDb.Name = order.Name;
                _orderDb.Address = order.Address;
                _orderDb.City = order.City;
                _orderRepository.Commit();
            }

            return _orderDb;
         }

        public int Delete(int id)
        {
            Orders _orderDb = _orderRepository.GetSingle(id);

            if (_orderDb == null)
            {
                return 0;
            }
            else
            {
                IEnumerable<Item> _items = _itemRepository.FindBy(s => s.CreatorId == id);

                foreach (var item in _items)
                {
                    _itemRepository.Delete(item);
                }

                _orderRepository.Delete(_orderDb);

                _orderRepository.Commit();

                return 1;
            }
        }

        #endregion
    }
}
