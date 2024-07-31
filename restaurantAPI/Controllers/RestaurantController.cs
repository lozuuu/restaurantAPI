using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantAPI.Entities;
using restaurantAPI.Models;
using restaurantAPI.Services;

namespace restaurantAPI.Controllers

{
    [Route("restaurant")]

    public class RestaurantController: ControllerBase
    {
        public IRestaurantService _restaurantService { get; }

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }


        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {

            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            /*if(!ModelState.IsValid)
            { return  BadRequest(ModelState); }*/
            var id =_restaurantService.Create(dto); 

            return Created($"restaurant/{id}" ,null);
        }


        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
                
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

    
