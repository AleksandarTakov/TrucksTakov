using System.ComponentModel.DataAnnotations;

namespace TrucksTakov.Models.Truck
{
    public class TruckIndexVM
    {
        [Key]
        public int Id { get; set; }
   
        
        // [Display(Name = "Truck Name")]

        //public string TruckName { get; set; }

        public int ManufacturerId { get; set; }
        [Display(Name = "Manufacturer")]

        public string ManufacturerName { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }

        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Image")]
        
        public string Image { get; set; }
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Display(Name = "Engine")]
        public string Engine { get; set; }
        [Display(Name = "Loadcapacity")]
        public int Loadcapacity { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Discount")]
        public decimal  Discount { get; set; }
    }
}
