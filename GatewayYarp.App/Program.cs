var builder = WebApplication.CreateBuilder(args);


builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();


app.MapGet("/", () => "Yarp started");
app.MapReverseProxy();
app.Run();
app.Run();