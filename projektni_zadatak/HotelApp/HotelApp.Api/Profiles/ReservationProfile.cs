using AutoMapper;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Services;

namespace HotelApp.Api.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationDto, Reservation>();

            CreateMap<Reservation, ReservationInfoDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Room.Hotel.Name))
                .ForMember(dest => dest.ReservationStatus, opt => opt.MapFrom(src => src.ReservationStatus.Name));

            CreateMap<ExternalApiDto, Reservation>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.ReservationNumber))
                .ForMember(dest => dest.ReservationStatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => 22));
        }
    }
}
