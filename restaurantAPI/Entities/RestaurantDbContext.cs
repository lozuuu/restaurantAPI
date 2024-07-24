namespace restaurantAPI.Entities
{
using Microsoft.EntityFrameworkCore;
    public class RestaurantDbContext : DbContext
    {
    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Dish> Dishes { get; set; }

    public DbSet<Address> Address { get; set; }

        private string _connectionString =
       "Server = localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
        .Property(r => r.Name)
        .IsRequired()
        .HasMaxLength(25);

        modelBuilder.Entity<Dish>()
            .Property(d => d.Name)
            .IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
}

