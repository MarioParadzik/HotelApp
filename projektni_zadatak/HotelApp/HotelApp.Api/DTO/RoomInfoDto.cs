using FluentValidation;

namespace HotelApp.Api.DTO
{
    public class RoomInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal Price { get; set; }
        public string HotelName { get; set; }
        public string LocationName { get; set; }
    }

    public class RoomInfoDtoValidator : AbstractValidator<RoomInfoDto>
    {
        public RoomInfoDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.NumberOfBeds).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.HotelName).NotEmpty();
            RuleFor(x => x.LocationName).NotEmpty();
        }
    }
}
