using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantAPI.Entities;
using restaurantAPI.Exceptions;
using restaurantAPI.Models;
using restaurantAPI.Models;

namespace restaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        public List<DishDto> GetAllDishes(int restaurantId);
        public DishDto GetDishById([FromRoute] int restaurantId, [FromRoute] int dishId);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        { 
            var restaurant = _context.Restaurants.FirstOrDefault(r =>  r.Id == restaurantId);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var dishEntity = _mapper.Map<Dish>(dto);

            dishEntity.RestaurantId = restaurantId;

            _context.Dishes.Add(dishEntity);

            _context.SaveChanges();

            return dishEntity.Id;

        }

        public List<DishDto> GetAllDishes(int restaurantId) 
        {
            var restaurant = _context.Restaurants.
                Include(r => r.Dishes).
                FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            var dishDtos = _mapper.Map <List<DishDto>>(restaurant.Dishes);

            return dishDtos;
        }

        public DishDto GetDishById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {

            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var dish = _context.Dishes.FirstOrDefault( d => d.Id == dishId);
            if(dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("dish not found");
            }
            var dishDto = _mapper.Map<DishDto>(dish);


            return dishDto;

        }

    }
}
