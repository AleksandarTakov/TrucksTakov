using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrucksTakov.Domain
{
    public class Truck
    {
        public int Id { get; set; }
        
       // [MaxLength(30)]
        //public string TruckName { get; set; }

       // [Required]
        [MaxLength(30)]
        public string ManufacturerName { get; set; }

        //[Required]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        //[Required]
        public string Model {get; set; }
       // [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        //[Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Image { get; set; }

       // [Required]
        [Range(2019, 2023)]
        public int Year { get; set; }

        //[Required]
        public string Engine { get; set; }
       // [Required]
        [Range(1000, 5000)]
        public int Loadcapacity { get; set; }
        [Required]
        [Range(0, 5000)]
        public int Quantity { get; set; }
       // [Required]
        public decimal Price { get; set; }
       // [Required]
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
