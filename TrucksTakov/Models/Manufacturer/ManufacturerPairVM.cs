using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrucksTakov.Models.Manufacturer
{
    public class ManufacturerPairVM
  
    {
        public int Id { get; set; }
        [Display(Name = "Manufacturer")]
        public string Name { get; set; }
    }
}
