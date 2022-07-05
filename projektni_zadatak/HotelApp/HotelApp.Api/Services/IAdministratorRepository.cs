using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelApp.Api.Services
{
    public interface IAdministratorRepository
    {
        public Task<ICollection<User>> GetAdministrators();
        public Task<IdentityResult> CreateAdministratorAsync(CreateAdministratorDto user);
        public Task<IdentityResult> RemoveAdministratorAsync(int id);
    }
}
