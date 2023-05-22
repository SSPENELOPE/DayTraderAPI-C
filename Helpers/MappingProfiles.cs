using AutoMapper;
using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.Identity;
using DayTraderProAPI.Dtos;
using DayTraderProAPI.Models;

namespace DayTraderProAPI.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {

            CreateMap<AppUser, UserDto>();
            CreateMap<OrderEntity, OrderDto>();
            CreateMap<RegistrationDto, AppUser>();
            CreateMap<LoginDto, AppUser>();
            CreateMap<WatchlistEntity, WatchlistDto>();
        }
    }
}
