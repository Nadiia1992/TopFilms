using Microsoft.EntityFrameworkCore;
using TopFilms.Models;



var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FilmsContext>(options => options.UseSqlServer(connection));


builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Films}/{action=Index}/{id?}");

app.Run();