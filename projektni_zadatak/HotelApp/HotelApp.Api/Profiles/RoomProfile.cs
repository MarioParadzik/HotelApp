using AutoMapper;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;

namespace HotelApp.Api.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreateRoomDto, Room>();
            CreateMap<Room, RoomInfoDto>()
                .ForMember(dest => dest.HotelName,opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Hotel.Location.Name));

            CreateMap<Room, RoomDetailsDto>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Hotel.Location.Name))
                .ForMember(dest => dest.ContactMail, opt => opt.MapFrom(src => src.Hotel.ContactMail))
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.Hotel.ContactNumber))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Hotel.Adress));
        }
    }
}
