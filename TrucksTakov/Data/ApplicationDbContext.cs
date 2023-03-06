using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrucksTakov.Domain;
using TrucksTakov.Models.Truck;

namespace TrucksTakov.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TrucksTakov.Models.Truck.TruckCreateVM> TruckCreateVM { get; set; }
        public DbSet<TrucksTakov.Models.Truck.TruckIndexVM> TruckIndexVM { get; set; }
        public DbSet<TrucksTakov.Models.Truck.TruckEditVM> TruckEditVM { get; set; }
        public DbSet<TrucksTakov.Models.Truck.TruckDetailsVM> TruckDetailsVM { get; set; }
        public DbSet<TrucksTakov.Models.Truck.TruckDeleteVM> TruckDeleteVM { get; set; }
    }
}
