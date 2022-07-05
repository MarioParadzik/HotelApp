using HotelApp.Api.DTO;

namespace HotelApp.Api.Services
{
    public interface ITokenRepository
    {
        Task<string> GenerateToken(string email);
    }
}
