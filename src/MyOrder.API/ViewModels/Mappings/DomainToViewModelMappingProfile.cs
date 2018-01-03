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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Item, ItemViewModel>();
                //.ForMember(dest1 => dest1.Name1, m1 => m1.MapFrom(src1 => src1.Order.Name));

                //cfg.CreateMap<Item, ItemDetailsViewModel>()
                //.ForMember(vm => vm.Creator, map => map.MapFrom(s => s.Order.Name))
                //.ForMember(vm => vm.Attendees, map => map.UseValue(new List<OrderViewModel>()))
                //.ForMember(vm => vm.Status, map => map.MapFrom(s => ((OrderStatus)s.Status).ToString()))
                //.ForMember(vm => vm.Type, map => map.MapFrom(s => ((OrderType)s.Type).ToString()))
                //.ForMember(vm => vm.Statuses, map => map.UseValue(Enum.GetNames(typeof(OrderStatus)).ToArray()))
                //.ForMember(vm => vm.Types, map => map.UseValue(Enum.GetNames(typeof(OrderType)).ToArray()));

                cfg.CreateMap<Orders, OrderViewModel>();
                //.ForMember(vm => vm.Items,  map => map.MapFrom(u => u.Items.Count())); 
                // You dont need to map the property explicitly, automapper interprets the property name and auto map Projects.Count()
                // to ProjectsCount
            });
        }
    }
}
