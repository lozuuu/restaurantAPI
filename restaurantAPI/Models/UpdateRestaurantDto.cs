using System.ComponentModel.DataAnnotations;

namespace restaurantAPI.Models
{
    public class UpdateRestaurantDto
    {
        
        [MaxLength(25)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool HasDelivery { get; set; }
    }
}
