using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ItemsAPI.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Entities.Item, Models.ItemWithoutRatingDto>();
            CreateMap<Entities.Item, Models.ItemDto>();
        }
    }
}
