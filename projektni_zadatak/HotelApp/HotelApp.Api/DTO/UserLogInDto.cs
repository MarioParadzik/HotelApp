using FluentValidation;
using HotelApp.Api.Helpers;

namespace HotelApp.Api.DTO
{
    public class UserLogInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLogInDtoValidator : AbstractValidator<UserLogInDto>
    {
        public UserLogInDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}