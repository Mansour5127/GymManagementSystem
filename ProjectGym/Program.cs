using GymManagementSystem.DataAccess.Data.Contexts;
using GymManagementSystem.DataAccess.Data.Seeder;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddGymDataAccess(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
} 

app.MapStaticAssets();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();

    await dbContext.Database.MigrateAsync();

    await DataBaseSeeder.SeedAllAsync(dbContext);
}
app.Run();