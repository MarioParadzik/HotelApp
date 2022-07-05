using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Api.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<User> userManager, HotelDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if(user == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            return user;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<bool> IsUniqueEmail(string email, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            return !await _context.Users.Where(x => x.Email == email).AnyAsync(token);
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto registerUser)
        {
            ArgumentNullException.ThrowIfNull(registerUser, nameof(registerUser));
            var user = _mapper.Map<User>(registerUser);
            var create = await _userManager.CreateAsync(user, registerUser.Password);
            await _userManager.AddToRoleAsync(user, Role.RegisteredUser);

            return create;

        }
        public async Task<bool> UserInRoleAsync(User user, string role)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(User user, string role)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            return await _userManager.AddToRoleAsync(user, role);
        }

        
    }
}
