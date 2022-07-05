using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Helpers;

namespace HotelApp.Api.Services
{
    public interface IHotelRepository
    {
        public IEnumerable<HotelInfoDto> GetHotels();
        public IEnumerable<int> GetHotelManagersHotels();
        public HotelInfoDto GetHotelById(int id);
        public Task<Hotel> RegisterHotelAsync(HotelRegistrationDto hotel);
        public Hotel UpdateHotel(HotelUpdateDto hotel, int id);
        public ICollection<HotelInfoDto> GetUnconfirmedHotels();
        public PagedList<RoomInfoDto> GetRoomsByHotelId( RoomsResourceParameters roomsResourceParameters, int id);
        public Hotel UpdateHotelStatus(int id, int statusId);
    }
}
