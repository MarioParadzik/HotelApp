using AutoMapper;
using HotelApp.Api.DbContexts;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using HotelApp.Api.Helpers;

namespace HotelApp.Api.Services
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;

        public ConfigurationRepository(HotelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Configuration GetConfigurationById(int id)
        {
            var configuration = _context.Configurations.FirstOrDefault(x => x.Id == id);
            if (configuration == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            return configuration;
        }

        public ICollection<Configuration> GetConfigurations()
        {
            return _context.Configurations.ToList();
        }

        public Configuration UpdateConfiguration(int id, string configurationInfo)
        {
            Configuration configuration = GetConfigurationById(id);
            configuration.Value = configurationInfo;
            _context.SaveChanges();
            return configuration;
        }

        public int GetDeadline()
        {
            var configuration = _context.Configurations.FirstOrDefault(c => c.Key == Configuration.Deadline);
            if(configuration == null) throw new BadRequestException("This configuration property is requiered!");
            return IntParser.parse(configuration.Value);
        }
    }
}
