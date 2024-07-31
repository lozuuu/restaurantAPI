namespace restaurantAPI.Entities
{
    using Microsoft.EntityFrameworkCore;
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Address> Address { get; set; }

        private readonly IConfiguration _configuration;
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options, IConfiguration configuration)
                : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasColumnType("decimal(18,2)"); // Specify precision and scale

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .HasMaxLength(50);

             
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

}


