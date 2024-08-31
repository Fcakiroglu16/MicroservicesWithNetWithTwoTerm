using Docker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Migration.Service
{
    public class Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceProvider serviceProvider)
        : BackgroundService
    {
        private readonly ILogger<Worker> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync(stoppingToken);
        }
    }
}