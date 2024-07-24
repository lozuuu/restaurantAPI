using restaurantAPI.Entities;

namespace restaurantAPI
{
    public class RestaurandSeeder
    {
        private readonly RestaurantDbContext _dbContext;


        public RestaurandSeeder(RestaurantDbContext dbContext)
        {

            _dbContext = dbContext;

            if (_dbContext.Database.CanConnect())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = getRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();

                }
            }
        }
    }   

    private IEnumerable<Restaurant> GetRestaurants()
    {

    }
}
