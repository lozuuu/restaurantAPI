using AutoMapper;
using Microsoft.EntityFrameworkCore;
using restaurantAPI.Entities;
using restaurantAPI.Models;

namespace restaurantAPI.Services
{
    public interface IRestaurantService
    {
        RestaurantDbContext DbContext { get; }

        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public RestaurantDbContext DbContext { get; }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                    .Restaurants
                    .Include(r => r.Addres)
                    .Include(r => r.Dishes)
                    .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return null;

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

    }


}
