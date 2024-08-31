using Docker.API.Models;
using Microsoft.EntityFrameworkCore;
using Migration.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
var host = builder.Build();
host.Run();