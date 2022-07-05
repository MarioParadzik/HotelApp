using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HotelApp.Api.Services
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;


        public AdministratorRepository(UserManager<User> userManager, IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            
        }

        public async Task<IdentityResult> CreateAdministratorAsync(CreateAdministratorDto administrator)
        {
            var user = _mapper.Map<User>(administrator);
            var create = await _userManager.CreateAsync(user, administrator.Password);
            await _userManager.AddToRoleAsync(user, Role.Administrator);

            return create;
        }

        public async Task<ICollection<User>> GetAdministrators()
        {
            return await _userManager.GetUsersInRoleAsync(Role.Administrator);
        }

        public async Task<IdentityResult> RemoveAdministratorAsync(int id)
        {
            User user = _userRepository.GetUserById(id);
            if (user == null) throw new BadRequestException("User with given id does not exist.");
            return await _userManager.DeleteAsync(user);
        }

    }
}
