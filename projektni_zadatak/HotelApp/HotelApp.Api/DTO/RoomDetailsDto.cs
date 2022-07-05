using FluentValidation;

namespace HotelApp.Api.DTO
{
    public class RoomDetailsDto: RoomInfoDto
    {
        public string ContactMail { get; set; }
        public string ContactNumber { get; set; }
        public string Adress { get; set; }
    }

    public class RoomDetailsDtoValidator : AbstractValidator<RoomDetailsDto>
    {
        public RoomDetailsDtoValidator()
        {
            RuleFor(x => x.ContactMail).NotEmpty();
            RuleFor(x => x.ContactNumber).NotEmpty();
            RuleFor(x => x.Adress).NotEmpty();
        }
    }
}
