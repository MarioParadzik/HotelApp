using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using HotelApp.Api.Filter;
using HotelApp.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelApp.Api.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISortHelper<Reservation> _sortHelper;
        private readonly IRoomRepository _roomRepository;
        private readonly IConfigurationRepository _configRepository;
        private readonly ILogger _logger;
        private readonly HttpClient _client;


        public ReservationRepository(HotelDbContext context, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepo, IMapper mapper, ISortHelper<Reservation> sortHelper, IRoomRepository roomRepo,
            IConfigurationRepository configRepo, ILogger<ReservationRepository> logger, IHttpClientFactory clientFactory)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepo;
            _mapper = mapper;
            _sortHelper = sortHelper;
            _roomRepository = roomRepo;
            _configRepository = configRepo;
            _logger = logger;
            _client = clientFactory.CreateClient("ExternalApi");

        }

        public Reservation AddReservation(ReservationDto reservationInfo)
        {
            var currentUser = IntParser.parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userRepository.GetUserById(currentUser);
            var reservation = _mapper.Map<Reservation>(reservationInfo);
            reservation.DateCreated = DateTime.Now;
            reservation.UserId = currentUser;
            reservation.ReservationStatusId = ReservationStatus.InProcess;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }


        public ICollection<ReservationInfoDto> GetReservationsForCurrentUser()
        {
            var currentUser = IntParser.parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userRepository.GetUserById(currentUser);
            var reservations = _context.Reservations.Include(r => r.Room).ThenInclude(h => h.Hotel).Where(x => x.UserId == user.Id && x.ReservationStatusId != ReservationStatus.Canceled).ToList();

            var reservationInfo = _mapper.Map<IEnumerable<ReservationInfoDto>>(reservations).ToList();
            checkCancelation(reservationInfo);
            return reservationInfo;
        }

        private void checkCancelation(List<ReservationInfoDto> reservationInfo)
        {
            foreach(var reservation in reservationInfo)
            {
                DateTime now = DateTime.Now;
                DateTime dateFrom = reservation.DateFrom;
                if(dateFrom > now && (dateFrom - now).TotalDays > _configRepository.GetDeadline())
                {
                    reservation.CanCancel = true;
                }
                else
                {
                    reservation.CanCancel = false;
                }
             
            }
        }

        public Reservation GetReservationById(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);
            if (reservation == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            return reservation;
        }

        public ICollection<ReservationStatus> GetReservationStatuses()
        {
            return _context.ReservationStatuses.ToList();
        }

        public Reservation UpdateReservationStatus(StatusIdWrapper statusId, int reservationId)
        {
            var reservation = GetReservationById(reservationId);
            if(reservation == null) throw new RecordNotFoundException($"Record with id {reservationId} does not exist.");
            reservation.ReservationStatusId = statusId.StatusId;
            _context.SaveChanges();
            return reservation;
        }

        public PagedList<ReservationInfoDto> GetReservations(ReservationResourceParameters reservationResourceParameters)
        {

            var reservationsQuery = _context.Set<Reservation>().AsQueryable();
            if (reservationResourceParameters.ReservationStatusIds != null)
            {
                reservationsQuery = reservationsQuery.Where(r => reservationResourceParameters.ReservationStatusIds.Contains(r.ReservationStatusId));
            }

            if (reservationResourceParameters.HotelIds != null)
            {
                reservationsQuery = reservationsQuery.Include(r => r.Room).Where(r => reservationResourceParameters.HotelIds.Contains(r.Room.HotelId));
            }

            reservationsQuery = _sortHelper.ApplySort(reservationsQuery, reservationResourceParameters.OrderBy);
            reservationsQuery = reservationsQuery.Include(r => r.Room).Include(r => r.ReservationStatus);
            var reservations =  reservationsQuery.ToPagedList(reservationResourceParameters.PageNumber, reservationResourceParameters.PageSize);

            return new PagedList<ReservationInfoDto>(_mapper.Map<IEnumerable<ReservationInfoDto>>(reservations.Records), reservations.TotalCount, reservations.CurrentPage, reservations.PageSize);
        }

        public bool IsFreeDate(DateTime dateFrom, DateTime dateTo, int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null) throw new RecordNotFoundException($"Record with id {roomId} does not exist.");

            return !room.Reservations.Where(res => res.DateTo > dateFrom &&
                                                    res.DateFrom < dateTo).Any();
        }
    }
}
