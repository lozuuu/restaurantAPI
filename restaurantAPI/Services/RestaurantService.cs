using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using restaurantAPI.Entities;
using restaurantAPI.Exceptions;
using restaurantAPI.Models;

namespace restaurantAPI.Services
{
    public interface IRestaurantService
    {
        RestaurantDbContext DbContext { get; }

        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
         
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public RestaurantDbContext DbContext { get; }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                    .Restaurants
                    .Include(r => r.Addres)
                    .Include(r => r.Dishes)
                    .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");


            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;


        }

        public IEnumerable<RestaurantDto> GetAll()
        {

            var restaurants = _dbContext
                        .Restaurants
                        .Include(r => r.Addres)
                        .Include(r => r.Dishes)
                        .ToList();

            var RestaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return RestaurantDtos;  
        }

        public int Create(CreateRestaurantDto dto)
        {
           

            var restaurant = _mapper.Map<Restaurant>(dto);
            
            _dbContext.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;

        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = _dbContext
                    .Restaurants
                    .FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges(true);

            
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant Not found");

            restaurant.Name = dto.Name ?? restaurant.Name;
            restaurant.Description = dto.Description ?? restaurant.Description;
            restaurant.HasDelivery = dto.HasDelivery;


            _dbContext.SaveChanges();
            

        }


    }


}
