using HotelApp.Api.Helpers;

namespace HotelApp.Api.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScope;
        public IConfiguration Configuration { get; }

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceScope, IConfiguration configuration)
        {
            _logger = logger;
            _serviceScope = serviceScope;
            Configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, 0, IntParser.parse(Configuration.GetSection("SyncTimer:miliseconds").Value));
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            using var scope = _serviceScope.CreateScope();
            var syncRepo = scope.ServiceProvider.GetRequiredService<ISyncReservationRepository>();
            syncRepo.SyncExternalReservations();
        
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
