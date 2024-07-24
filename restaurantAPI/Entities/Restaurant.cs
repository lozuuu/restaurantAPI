namespace restaurantAPI.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
         public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery { get; set; }

        public string ContatEmail { get; set; }

        public string ContactNumber {  get; set; } 

        public int AddressId { get; set; }

         public virtual Addres Addres { get; set; }

        public virtual List<Dish> Dishes { get; set; }


    }
}
