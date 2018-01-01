using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrder.API.Core;
using MyOrder.API.ViewModels;
using MyOrder.Data.Abstract;
using MyOrder.Model;

namespace AngularWebpackVisualStudio.Controllers
{
    // [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        int page = 1;
        int pageSize = 10;
        public OrderController(IOrderRepository orderRepository,
                               IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        public IActionResult Get()
        {
            // http://localhost:5000/api/Order
            // Content-Type:application/json
            // Pagination: 1,10
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalOrders = _orderRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

            IEnumerable<Orders> _orders = _orderRepository
                .AllIncluding(u => u.ItemsCreated)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<OrderViewModel> _orderVM = Mapper.Map<IEnumerable<Orders>, IEnumerable<OrderViewModel>>(_orders);

            Response.AddPagination(page, pageSize, totalOrders, totalPages);

            return new OkObjectResult(_orderVM);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            Orders _order = _orderRepository.GetSingle(u => u.Id == id, u => u.ItemsCreated);

            if (_order != null)
            {
                OrderViewModel _userVM = Mapper.Map<Orders, OrderViewModel>(_order);
                return new OkObjectResult(_userVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/items", Name = "GetOrderItems")]
        public IActionResult GetOrders(int id)
        {
            IEnumerable<Item> _orderItems = _itemRepository.FindBy(s => s.CreatorId == id);

            if (_orderItems != null)
            {
                IEnumerable<ItemViewModel> _orderItemsVM = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(_orderItems);
                return new OkObjectResult(_orderItemsVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]OrderViewModel order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Orders _newOrder = new Orders { Name = order.Name, Address = order.Address, City = order.City };

            _orderRepository.Add(_newOrder);
            _orderRepository.Commit();

            order = Mapper.Map<Orders, OrderViewModel>(_newOrder);

            CreatedAtRouteResult result = CreatedAtRoute("GetOrder", new { controller = "Order", id = order.Id }, order);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Orders _orderDb = _orderRepository.GetSingle(id);

            if (_orderDb == null)
            {
                return NotFound();
            }
            else
            {
                _orderDb.Name = order.Name;
                _orderDb.Address = order.Address;
                _orderDb.City = order.City;
                _orderRepository.Commit();
            }

            order = Mapper.Map<Orders, OrderViewModel>(_orderDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Orders _orderDb = _orderRepository.GetSingle(id);

            if (_orderDb == null)
            {
                return new NotFoundResult();
            }
            else
            {                
                IEnumerable<Item> _items = _itemRepository.FindBy(s => s.CreatorId == id);

                foreach (var schedule in _items)
                {                    
                    _itemRepository.Delete(schedule);
                }

                _orderRepository.Delete(_orderDb);

                _orderRepository.Commit();

                return new NoContentResult();
            }
        }
    }
}