using Microsoft.AspNetCore.Mvc;
using restaurantAPI.Entities;
using restaurantAPI.Models;

namespace restaurantAPI.Controllers

{
    [Route("restaurant")]

    public class RestaurantController: ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;       
        }

        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .ToList();
            var restaurantsDtos = restaurants.Select(r => new RestaurantDto()
            {
                

            }
            return Ok(restaurantsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(restaurant);
            }
        }
    }
   
}

    
