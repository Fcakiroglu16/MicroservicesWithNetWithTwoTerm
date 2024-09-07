using App.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(builder.Environment.ContentRootPath));

builder.Services.AddHttpClient<MicroserviceOneService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("OneMicroservice")["BaseUrl"]!);
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.Cookie.Name = "ResourceOwnerWebCookieName";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.LoginPath = "/Home/SignIn";
    opts.AccessDeniedPath = "/Home/AccessDenied";
});

//builder.Services.AddAuthentication(options => { options.DefaultScheme = "bayi-schema"; }).AddCookie("bayi-schema",
//    opts =>
//    {
//        opts.Cookie.Name = "BayiCookieName";
//        opts.Cookie.Expiration = TimeSpan.FromDays(60);
//    });


//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//    }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts => { opts.Cookie.Name = "AppWebCookieName"; })
//    .AddOpenIdConnect(opts =>
//    {
//        var clientId = builder.Configuration.GetSection("Client")["Id"]!;
//        var clientSecret = builder.Configuration.GetSection("Client")["Secret"]!;
//        var authority = builder.Configuration.GetSection("Client")["Authority"]!;

//        opts.Authority = authority;
//        opts.RequireHttpsMetadata = false;
//        opts.ClientId = clientId;
//        opts.ClientSecret = clientSecret;
//        opts.ResponseType = "code";
//        opts.SaveTokens = true;
//        opts.Scope.Add("profile email address phone roles");
//        opts.GetClaimsFromUserInfoEndpoint = true;
//    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();