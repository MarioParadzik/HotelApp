using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;

namespace HotelApp.Api.Services
{
    public class SyncReservationRepository : ISyncReservationRepository
    {
        private readonly IMapper _mapper;
        private readonly IExternalApiClient _externalApi;
        private readonly IReservationRepository _reservationRepository;
        private readonly HotelDbContext _context;


        public SyncReservationRepository(IExternalApiClient exteralApi, IMapper mapper, IReservationRepository reservationRepo, HotelDbContext context)
        {
            _externalApi = exteralApi;
            _mapper = mapper;
            _reservationRepository = reservationRepo;
            _context = context;

        }

        public async Task<List<Reservation>> SyncExternalReservations()
        {
            List<ExternalApiDto> externalReservationsList = await _externalApi.GetReservations();
            var filteredReservations = new List<ExternalApiDto>();
            foreach (var item in externalReservationsList)
            {
                if (_reservationRepository.IsFreeDate(item.DateFrom, item.DateTo, item.RoomId)) filteredReservations.Add(item);
            }

            var externalReservations = _mapper.Map<IEnumerable<Reservation>>(filteredReservations);
            if (externalReservations.Count() == 0) throw new BadRequestException("No new Reservations to add!");
            _context.Reservations.AddRange(externalReservations);
            _context.SaveChanges();
            return externalReservations.ToList();
        }

    }
}
