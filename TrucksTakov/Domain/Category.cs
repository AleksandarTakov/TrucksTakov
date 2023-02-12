using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TrucksTakov.Domain
{
    public class Category
    {
            public int Id { get; set; }
            [Required]
            [MaxLength(30)]
            public string CategotyName { get; set; }
            public virtual IEnumerable<Truck> Trucks { get; set; } = new List<Truck>();
        }
    }


