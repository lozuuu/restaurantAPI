using Microsoft.EntityFrameworkCore;
using restaurantAPI;
using restaurantAPI.Entities;
using restaurantAPI.Services;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NLog.Web;
using restaurantAPI.Middleware;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
//konmfiguracja NLoga
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers();
//builder.Services.AddDbContext<RestaurantDbContext>(); //to coœ do bazy - doedukowaæ sie co to za gowno 
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<IDishService, DishService>();





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
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}


app.UseAuthorization();

app.MapControllers();

app.Run();
