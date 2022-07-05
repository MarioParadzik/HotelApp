using AutoMapper;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;

namespace HotelApp.Api.Profiles
{
    public class UserProfile : Profile
    {
       public UserProfile()
        {
            CreateMap<UserRegistrationDto, User>().ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateAdministratorDto, User>().ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email));
        }
        

    }
}
