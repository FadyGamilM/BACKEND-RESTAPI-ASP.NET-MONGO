using catalog.Models;
using catalog.DTOs;
using AutoMapper;
namespace catalog.Profiles
{
   public class ItemProfile : Profile
   {
      public ItemProfile()
      {
         CreateMap<CreateItemDto, Item>();
         CreateMap<ReadItemDto, Item>();
         CreateMap<Item, CreateItemDto>();
         CreateMap<Item, ReadItemDto>();
      }
   }
}