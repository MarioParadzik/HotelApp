using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Helpers;

namespace HotelApp.Api.Services
{
    public interface IRoomRepository
    {
        public Room GetRoomById(int id);
        public Room CreateRoom(CreateRoomDto roomForCreation);
        public Room UpdateRoom(CreateRoomDto roomForCreation, int id);
        public PagedList<RoomInfoDto> GetRooms(RoomsResourceParameters roomsResourceParameters);
        public PagedList<RoomInfoDto> GetRoomsForHotel(RoomsResourceParameters roomsResourceParameters, int id);
        public Room DeleteRoomById(int id);
        public List<int> GetNumberOfBeds();
        public RoomDetailsDto GetRoomInfoById(int id);
    }
}
