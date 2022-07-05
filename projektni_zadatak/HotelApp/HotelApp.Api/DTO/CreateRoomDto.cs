using FluentValidation;
using HotelApp.Api.Services;

namespace HotelApp.Api.DTO
{
    public class CreateRoomDto
    {
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
    }

    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.NumberOfBeds).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.HotelId).NotEmpty();
        }
    }
}
