using AutoMapper;
using restaurantAPI.Entities;
using restaurantAPI.Models;

namespace restaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile() 
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Addres.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Addres.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Addres.PostalCode));
            CreateMap<Dish, DishDto>();
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Addres, c => c.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

        }
    }
}
