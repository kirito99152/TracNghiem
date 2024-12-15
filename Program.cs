using Microsoft.EntityFrameworkCore;
using TracNghiem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(180);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/ErrorHandle/Forbidden";
        options.LoginPath = "/Account/Login";
        options.EventsType = typeof(AuthenticationEvents);
    });
builder.Services.AddScoped<AuthenticationEvents>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<TracNghiem.Models.TracNghiemContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Host"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
