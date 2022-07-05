using FluentValidation;
using HotelApp.Api.Entities;
using HotelApp.Api.Helpers;
using HotelApp.Api.Services;

namespace HotelApp.Api.DTO
{
    public class HotelUpdateDto
    {
        public string Name { get; set; }
        public string ContactMail { get; set; }
        public string ContactNumber { get; set; }
        public string Adress { get; set; }
        public int LocationId { get; set; }
        public int HotelStatusId { get; set; }
    }
    public class HotelUpdateDtoValidator : AbstractValidator<HotelUpdateDto>
    {
        public HotelUpdateDtoValidator(ILocationRepository locationRepo)
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.ContactMail).NotEmpty().EmailAddress();
            RuleFor(x => x.ContactNumber).NotEmpty();
            RuleFor(x => x.Adress).NotEmpty();
            RuleFor(x => x.LocationId).NotEmpty()
                .MustAsync((x, token) => locationRepo.LocationExists(x, token)).WithMessage(ErrorMessages.LocationNotExisting);
            RuleFor(x => x.HotelStatusId).NotEmpty();
        }
    }
}
