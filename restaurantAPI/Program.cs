using Microsoft.EntityFrameworkCore;
using restaurantAPI;
using restaurantAPI.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
Console.WriteLine("dupa)");


builder.Services.AddControllers();
//builder.Services.AddDbContext<RestaurantDbContext>(); //to coœ do bazy - doedukowaæ sie co to za gowno 
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<RestaurantSeeder>();



var app = builder.Build();

//var scope = app.Services.CreateScope();
//var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>(); 

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
    try
    {
        seeder.Seed();
        //Console.WriteLine(");
        System.Diagnostics.Debug.WriteLine("Data seeding completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during seeding: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
//seeder.Seed();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
