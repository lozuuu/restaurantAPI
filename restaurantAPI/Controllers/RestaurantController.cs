using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantAPI.Entities;
using restaurantAPI.Models;
using restaurantAPI.Services;

namespace restaurantAPI.Controllers

{
    [Route("restaurant")]
    [ApiController]

    public class RestaurantController: ControllerBase
    {
        public IRestaurantService _restaurantService { get; }

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {

             var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
           
            var id =_restaurantService.Create(dto); 

            return Created($"restaurant/{id}" ,null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

           
            return NoContent();
        }

        [HttpPut("{id}")]
         public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute]int id)
        {
          

            _restaurantService.Update(id, dto);
           
            return Ok();
        }



        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
                
            return Ok(restaurant);
            
        }
    }
   
}

    
