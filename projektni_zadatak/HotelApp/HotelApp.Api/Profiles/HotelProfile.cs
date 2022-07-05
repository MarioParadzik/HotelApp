using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Helpers;

namespace HotelApp.Api.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelRegistrationDto, Hotel>();
            CreateMap<HotelUpdateDto, Hotel>();
            CreateMap<Hotel, HotelInfoDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Name))
                .ForMember(dest => dest.HotelStatus, opt => opt.MapFrom(src => src.HotelStatus.Name));
        }
    }
}
