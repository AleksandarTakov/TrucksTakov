using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrucksTakov.Models.Truck
{
    public class TruckDetailsVM
    {
        [Key]
        public int Id { get; set; }

        

        public int ManufacturerId { get; set; }
        [Display(Name = "Manufaturer")]
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
        public decimal Discount { get; set; }

    }
}
