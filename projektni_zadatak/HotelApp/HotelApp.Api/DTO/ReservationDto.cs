using FluentValidation;
using HotelApp.Api.Helpers;
using HotelApp.Api.Services;

namespace HotelApp.Api.DTO
{
    public class ReservationDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
        public int RoomId { get; set; }

    }

    public class ReservationDtoValidator : AbstractValidator<ReservationDto>
    {
        public ReservationDtoValidator(IReservationRepository reservationRepo)
        {
            RuleFor(x => x).Must((x) => reservationRepo.IsFreeDate(x.DateFrom, x.DateTo, x.RoomId)).WithMessage(ErrorMessages.DateNotFree);
            RuleFor(x => x.DateFrom).NotEmpty().GreaterThanOrEqualTo(DateTime.Now).LessThanOrEqualTo(x => x.DateTo);
            RuleFor(x => x.DateTo).NotEmpty().GreaterThanOrEqualTo(x => x.DateFrom);
            RuleFor(x => x.RoomId).NotEmpty();
        }
    }
}
