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
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            var id =_restaurantService.Create(dto); 

            return Created($"restaurant/{id}" ,null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _restaurantService.Delete(id);

            if (isDeleted) return NoContent();
           
            return NotFound();
        }

        [HttpPut("{id}")]
         public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _restaurantService.Update(id, dto);
            if(!isUpdated) return NotFound();
            return Ok();
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

    
