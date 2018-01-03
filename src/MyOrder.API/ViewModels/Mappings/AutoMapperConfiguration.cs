using AutoMapper;
using MyOrder.Model;
using System.Collections.Generic;

namespace MyOrder.API.ViewModels.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Item, ItemViewModel>()
                    .ForMember(dest => dest.Order, m => m.MapFrom(src => src.Order.Name ));

                // cfg.AddProfile<DomainToViewModelMappingProfile>();
                // cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}
