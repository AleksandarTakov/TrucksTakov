using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrucksTakov.Abstraction;
using TrucksTakov.Data;
using TrucksTakov.Domain;


namespace TrucksTakov.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ApplicationDbContext _context;
        public ManufacturerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            return _context.Manufacturers.Find(manufacturerId);
        }
        public List<Manufacturer> GetManufacturers()
        {
            List<Manufacturer> manufacturers = _context.Manufacturers.ToList();
            return manufacturers;
        }

     
        public List<Truck> GetTruckByManufacturer(int manufacturerId)
        {
            return _context.Trucks
                .Where(x => x.ManufacturerId == manufacturerId)
                .ToList();
        }
    }
}