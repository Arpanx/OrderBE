using AutoMapper;
using MyOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyOrder.API.ViewModels.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.Order, m => m.MapFrom(src => src.Order.Name));

            // ViewModelToDomainMappingProfile
            CreateMap<ItemViewModel, Item>();
                  // .ForMember(s => s.Order, map => map.UseValue("{id=1}"));
            //.ForMember(s => s.Attendees, map => map.UseValue(new List<Attendee>()));

            CreateMap<OrderViewModel, Orders>();
        }
    }
}
