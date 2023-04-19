using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrucksTakov.Domain
{
    public class Truck
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string ManufacturerName { get; set; }
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public string Model {get; set; }
        [MaxLength(30)]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Image { get; set; }
        [Range(2019, 2023)]
        public int Year { get; set; }
        public string Engine { get; set; }
        [Range(1000, 5000)]
        public int Loadcapacity { get; set; }
        [Required]
        [Range(0, 5000)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
