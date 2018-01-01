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
    // [Route("api/items")]
    [Route("api/[controller]")]
    public class ItemController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IItemRepository _itemRepository;        
        private readonly IOrderRepository _orderRepository;
        int page = 1;
        int pageSize = 4;

        public ItemController(IItemRepository scheduleRepository,                                    
                              IOrderRepository userRepository)
        {
            _itemRepository = scheduleRepository;            
            _orderRepository = userRepository;
        }

        public IActionResult Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalSchedules = _itemRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalSchedules / pageSize);

            IEnumerable<Item> _schedules = _itemRepository
                .AllIncluding(s => s.Creator)
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalSchedules, totalPages);

            IEnumerable<ItemViewModel> _schedulesVM = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(_schedules);

            return new OkObjectResult(_schedulesVM);
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(int id)
        {
            Item _schedule = _itemRepository
                .GetSingle(s => s.Id == id, s => s.Creator);

            if (_schedule != null)
            {
                ItemViewModel _scheduleVM = Mapper.Map<Item, ItemViewModel>(_schedule);
                return new OkObjectResult(_scheduleVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/items", Name = "GetItemsDetails")]
        public IActionResult GetScheduleDetails(int id)
        {
            Item _item = _itemRepository
                .GetSingle(s => s.Id == id, s => s.Creator);

            if (_item != null)
            {
                ItemDetailsViewModel _itemDetailsVM = Mapper.Map<Item, ItemDetailsViewModel>(_item);

                return new OkObjectResult(_itemDetailsVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]ItemViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Item _newItem = Mapper.Map<ItemViewModel, Item>(item);
            _newItem.DateCreated = DateTime.Now;

            _itemRepository.Add(_newItem);
            _itemRepository.Commit();

            item = Mapper.Map<Item, ItemViewModel>(_newItem);

            CreatedAtRouteResult result = CreatedAtRoute("GetItem", new { controller = "Item", id = item.Id }, item);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ItemViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Item _itemDb = _itemRepository.GetSingle(id);

            if (_itemDb == null)
            {
                return NotFound();
            }
            else
            {
                _itemDb.Title = item.Title;
                _itemDb.Location = item.Location;
                _itemDb.Description = item.Description;
                _itemDb.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.Status);
                _itemDb.Type = (OrderType)Enum.Parse(typeof(OrderType), item.Type);
                _itemDb.TimeStart = item.TimeStart;
                _itemDb.TimeEnd = item.TimeEnd;

                _itemRepository.Commit();
            }

            item = Mapper.Map<Item, ItemViewModel>(_itemDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}", Name = "RemoveItem")]
        public IActionResult Delete(int id)
        {
            Item _itemDb = _itemRepository.GetSingle(id);

            if (_itemDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _itemRepository.Delete(_itemDb);

                _itemRepository.Commit();

                return new NoContentResult();
            }
        }
    }
}