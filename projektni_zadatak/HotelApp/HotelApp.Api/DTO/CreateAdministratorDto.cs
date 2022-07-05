using FluentValidation;
using HotelApp.Api.Helpers;
using HotelApp.Api.Services;

namespace HotelApp.Api.DTO
{
    public class CreateAdministratorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateAdministratorDtoValidator : AbstractValidator<CreateAdministratorDto>
    {
        public CreateAdministratorDtoValidator(IUserRepository userRepo)
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .MustAsync((x, token) => userRepo.IsUniqueEmail(x, token)).WithMessage(ErrorMessages.EmailTaken);
            RuleFor(x => x.Password).Password();
        }
    }
}
