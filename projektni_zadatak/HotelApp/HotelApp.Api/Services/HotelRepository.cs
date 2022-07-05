using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using HotelApp.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelApp.Api.Services
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        public HotelRepository(HotelDbContext context, IHttpContextAccessor httpContextAccessor, IUserRepository userRepo, IMapper mapper, IRoomRepository roomRepo)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepo;
            _mapper = mapper;
            _roomRepository = roomRepo;
        }

        public IEnumerable<HotelInfoDto> GetHotels()
        {
            var user = IntParser.parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var hotels =  _context.HotelUser.Where(x => x.UserId == user)
                .SelectMany(x => _context.Hotels.Include(l => l.Location).Include(hs => hs.HotelStatus).Where(u => u.Id == x.HotelId));
            var hotelInfo = _mapper.Map<IEnumerable<HotelInfoDto>>(hotels).ToList();
            return hotelInfo;
        }

        public IEnumerable<int> GetHotelManagersHotels()
        {
            var user = IntParser.parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var hotels = _context.HotelUser.Where(x => x.UserId == user)
                .SelectMany(x => _context.Hotels.Where(u => u.Id == x.HotelId)).Select(h => h.Id).ToList();
            return hotels;
        }


        public HotelInfoDto GetHotelById(int id)
        {
            var hotel = _context.Hotels.Include(l => l.Location).Include(hs => hs.HotelStatus).FirstOrDefault(x => x.Id == id);
            if (hotel == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            var hotelInfo = _mapper.Map<HotelInfoDto>(hotel);
            return hotelInfo;
        }

        public async Task<Hotel> RegisterHotelAsync(HotelRegistrationDto hotel)
        {
            var currentUser = IntParser.parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userRepository.GetUserById(currentUser);
            bool isHotelManager = await _userRepository.UserInRoleAsync(user, Role.HotelManager);
            if(!isHotelManager)
            {
                await _userRepository.AddUserToRoleAsync(user, "Hotel Manager");
            }

            var transfer = _mapper.Map<Hotel>(hotel);
            transfer.HotelStatusId = HotelStatus.Unconfirmed;
            var hotelManager = new HotelUser();
            hotelManager.Hotel = transfer;
            hotelManager.User = user;

            _context.HotelUser.Add(hotelManager);
            _context.Hotels.Add(transfer);
            _context.SaveChanges();

            return transfer;
        }

        public Hotel UpdateHotel(HotelUpdateDto hotel, int id)
        {
            var hotelToUpdate = _context.Hotels.FirstOrDefault(x => x.Id == id);

            hotelToUpdate = _mapper.Map(hotel, hotelToUpdate);
            _context.Hotels.Update(hotelToUpdate);
            _context.SaveChanges();
            return hotelToUpdate;
             
        }

        public ICollection<HotelInfoDto> GetUnconfirmedHotels()
        {
            var hotels = _context.Hotels.Include(l => l.Location).Include(hs => hs.HotelStatus).Where(x => x.HotelStatusId == HotelStatus.Unconfirmed).ToList();
            var hotelInfo =  _mapper.Map<IEnumerable<HotelInfoDto>>(hotels).ToList();
            return hotelInfo;
        }

        public PagedList<RoomInfoDto> GetRoomsByHotelId(RoomsResourceParameters roomsResourceParameters, int id)
        {
            return _roomRepository.GetRoomsForHotel(roomsResourceParameters, id);
        }

        public Hotel UpdateHotelStatus(int id, int statusId)
        {
            if (statusId == HotelStatus.Unconfirmed) throw new BadRequestException("Cannot update hotel from unconfirmed to unconfirmed.");
            var hotel = _context.Hotels.FirstOrDefault(x => x.Id == id);
            hotel.HotelStatusId = statusId;
            _context.SaveChanges();
            return hotel;
        }
    }
}
