using HotelApp.Api.DTO;
using HotelApp.Api.Entities;

namespace HotelApp.Api.Services
{
    public interface IConfigurationRepository
    {
        public ICollection<Configuration> GetConfigurations();
        public Configuration GetConfigurationById(int id);
        public Configuration UpdateConfiguration(int id, string configurationInfo);
        public int GetDeadline();
    }
}
