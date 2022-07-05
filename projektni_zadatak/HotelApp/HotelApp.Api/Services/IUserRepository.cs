using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelApp.Api.Services
{
    public interface IUserRepository
    {
        public ICollection<User> GetUsers();
        public User GetUserById(int id);
        public Task<bool> IsUniqueEmail(string email, CancellationToken token = default);
        public Task<IdentityResult> RegisterUserAsync(UserRegistrationDto user);
        Task<bool> UserInRoleAsync(User user, string role);
        Task<IdentityResult> AddUserToRoleAsync(User user, string role);
    }
}
