using restaurantAPI.Entities;

namespace restaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext) 
            {
            _dbContext = dbContext;
        }

        public RestaurantDbContext DbContext { get; }

        public void Seed()
        {
             if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }    
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>();

            restaurants.Add(new Restaurant()
            {
                Name = "kfccc",
                Category = "Fastfood",
                Description = "dupa",
                ContactEmail = "kacper123@o2.pl",
                HasDelivery = true,
                ContactNumber = "312321321",

                Dishes = new List<Dish>()
        {
            new Dish()
            {
                Name = "birgir",
                Price = 1,
                Description = "grgeg",
            },

            new Dish()
            {
                Name = "bulka z paruwka",
                Price = 10.30M,
                Description = "grgeg",
            },

            new Dish()
            {
                Name = "japko",
                Price = 13.60M,
                Description = "japko takie slodziutkie",
            }
        },
                Addres = new Address()
                {
                    City = "Rzeszoww",
                    PostalCode = "35-084",
                    Street = "Podkarpacka",
                }
            });

            // Dodajemy kolejną restaurację
            restaurants.Add(new Restaurant()
            {
                Name = "McDonalds",
                Category = "Fastfood",
                Description = "Popular fast food chain",
                ContactEmail = "contact@mcdonalds.com",
                HasDelivery = true,
                ContactNumber = "123456789",

                Dishes = new List<Dish>()
        {
            new Dish()
            {
                Name = "Big Mac",
                Price = 15.00M,
                Description = "A big juicy burger",
            },

            new Dish()
            {
                Name = "French Fries",
                Price = 5.50M,
                Description = "Crispy golden fries",
            },

            new Dish()
            {
                Name = "Coke",
                Price = 3.00M,
                Description = "Chilled Coca Cola",
            }
        },
                Addres = new Address()
                {
                    City = "Warsaw",
                    PostalCode = "00-001",
                    Street = "Main Street",
                }
            });

            return restaurants;
        }



    }
}
