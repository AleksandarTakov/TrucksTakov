﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrucksTakov.Models.Category;
using TrucksTakov.Models.Manufacturer;

namespace TrucksTakov.Models.Truck
{
    public class TruckCreateVM
    {
        public TruckCreateVM()
        {
            Manufacturers = new List<ManufacturerPairVM>();
            Categories = new List<CategoryPairVM>();
        }
        [Key]
        public int Id { get; set; }

        
      //  [MaxLength(30)]
       // [Display(Name = "Truck Name")]
       // public string TruckName { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual List<ManufacturerPairVM> Manufacturers { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual List<CategoryPairVM> Categories { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Display(Name = "Engine")]
        public string Engine { get; set; }
        [Display(Name = "Loadcapacity")]
        public int Loadcapacity { get; set; }

        [Required]
        [Range(0, 5000)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
