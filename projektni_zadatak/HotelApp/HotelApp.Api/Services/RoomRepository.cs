using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using HotelApp.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Api.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISortHelper<Room> _sortHelper;

        public RoomRepository(HotelDbContext context, IMapper mapper, ISortHelper<Room> sortHelper)
        {
            _context = context;
            _mapper = mapper;
            _sortHelper = sortHelper;
        }

        public Room CreateRoom(CreateRoomDto roomToAdd)
        {
            var room = _mapper.Map<Room>(roomToAdd);
            if (room == null) throw new BadRequestException("Failed to add room.");
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return room;
        }

        public Room UpdateRoom(CreateRoomDto room, int id)
        {
            var roomToUpdate = GetRoomById(id);
            roomToUpdate = _mapper.Map(room, roomToUpdate);
            _context.Rooms.Update(roomToUpdate);
            _context.SaveChanges();
            return roomToUpdate;
        }

        public ICollection<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room GetRoomById(int id)
        {
            var room = _context.Rooms.Include(x => x.Reservations).FirstOrDefault(x => x.Id == id);
            if (room == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            return room;
            
        }

        public RoomDetailsDto GetRoomInfoById(int id)
        {
            var room = _context.Rooms.Include(x => x.Reservations).Include(x => x.Hotel).ThenInclude(l => l.Location).FirstOrDefault(x => x.Id == id);
            if (room == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            var roomInfo = _mapper.Map<RoomDetailsDto>(room);
            return roomInfo;
        }

        public PagedList<RoomInfoDto> GetRooms(RoomsResourceParameters roomsResourceParameters)
        {
            var roomsQuery = _context.Rooms.Include(r => r.Hotel).Where(r => roomsResourceParameters.HotelStatus == r.Hotel.HotelStatusId);
            roomsQuery = Filter(ref roomsQuery, roomsResourceParameters);
            roomsQuery = _sortHelper.ApplySort(roomsQuery, roomsResourceParameters.OrderBy);
            roomsQuery = roomsQuery.Include(x => x.Hotel).ThenInclude(l => l.Location);
            var rooms = roomsQuery.ToPagedList(roomsResourceParameters.PageNumber, roomsResourceParameters.PageSize);
            return new PagedList<RoomInfoDto>(_mapper.Map<IEnumerable<RoomInfoDto>>(rooms.Records), rooms.TotalCount, rooms.CurrentPage, rooms.PageSize);
        }

        public PagedList<RoomInfoDto> GetRoomsForHotel(RoomsResourceParameters roomsResourceParameters, int id)
        {
            var roomsQuery = _context.Rooms.Where(x => x.HotelId == id);
            roomsQuery = Filter(ref roomsQuery, roomsResourceParameters);
            roomsQuery = _sortHelper.ApplySort(roomsQuery, roomsResourceParameters.OrderBy);
            roomsQuery = roomsQuery.Include(x => x.Hotel).ThenInclude(l => l.Location);
            var rooms = roomsQuery.ToPagedList(roomsResourceParameters.PageNumber, roomsResourceParameters.PageSize);
            return new PagedList<RoomInfoDto>(_mapper.Map<IEnumerable<RoomInfoDto>>(rooms.Records), rooms.TotalCount, rooms.CurrentPage, rooms.PageSize);
        }

        public Room DeleteRoomById(int id)
        {
            var room = GetRoomById(id);
            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return room;
        }


        private IQueryable<Room> Filter(ref IQueryable<Room> rooms, RoomsResourceParameters roomsResourceParameters)
        {
            if (roomsResourceParameters.LocationIds is not null && roomsResourceParameters.LocationIds.Any())
            {
                rooms = rooms.Where(r => roomsResourceParameters.LocationIds.Contains(r.Hotel.LocationId));
            }
            if (roomsResourceParameters.NumberOfBeds is not null && roomsResourceParameters.NumberOfBeds.Any())
            {
                rooms = rooms.Where(r => roomsResourceParameters.NumberOfBeds.Contains(r.NumberOfBeds));
            }
            if (roomsResourceParameters.DateFrom != null && roomsResourceParameters.DateTo != null)
            {
                rooms = rooms.Where(r => r.Reservations.All(res => res.DateTo < roomsResourceParameters.DateFrom ||
                                                    res.DateFrom > roomsResourceParameters.DateTo));
            }
            return rooms;
        }

        public List<int> GetNumberOfBeds()
        {
            return _context.Rooms.OrderBy(x => x.NumberOfBeds).Select(x => x.NumberOfBeds).Distinct().ToList();
        }

    }
}
