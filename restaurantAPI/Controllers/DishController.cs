using Microsoft.AspNetCore.Mvc;
using restaurantAPI.Models;
using restaurantAPI.Services;

namespace restaurantAPI.Controllers
{
    [Route("restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost]

        public ActionResult Post([FromRoute]int restaurantId,[FromBody] CreateDishDto dto)
        {
           var newDishId = _dishService.Create(restaurantId,dto);

            return Created($"restaurant/{ restaurantId}/DishController/{ newDishId}",null);

        }

        [HttpGet("{dishId}")]

        public ActionResult<DishDto> GetDish([FromRoute]int restaurantId, [FromRoute] int dishId)
        {
            DishDto  dish = _dishService.GetDishById(restaurantId, dishId);
            return Ok(dish);

        }

        [HttpGet]
        public ActionResult<List<DishDto>> GetAllDish([FromRoute] int restaurantId)
        {
            var result = _dishService.GetAllDishes(restaurantId);
            return Ok(result);  

        }


    }
}
