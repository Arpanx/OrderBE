using AutoMapper;
using MyOrder.Model;
using System.Collections.Generic;

namespace MyOrder.API.ViewModels.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemViewModel, Item>()
                    .ForMember(s => s.Order, map => map.UseValue("1"));
                    //.ForMember(s => s.Attendees, map => map.UseValue(new List<Attendee>()));

                cfg.CreateMap<OrderViewModel, Orders>();
            });
        }
    }
}
