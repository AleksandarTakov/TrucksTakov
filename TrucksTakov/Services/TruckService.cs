using System;
using System.Collections.Generic;
using System.Linq;
using TrucksTakov.Data;
using TrucksTakov.Abstraction;
using TrucksTakov.Domain;

namespace TrucksTakov.Services
{
    public class TruckService : ITruckService
    {
        private readonly ApplicationDbContext _context;

        public TruckService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create( int manufacturerId,string model, int categoryId, string image, int year, string engine, int loadcapacity, int quantity, decimal price,string description, decimal discount)
        {
            Truck item = new Truck
            {
                //TruckName = name,
                Manufacturer = _context.Manufacturers.Find(manufacturerId),
                Model = model,
                Category = _context.Categories.Find(categoryId),
                Image = image,
                Year = year,
                Engine = engine,
                Loadcapacity= loadcapacity,
                Quantity = quantity,
                Price = price,
                Description = description,
                Discount = discount
            };

            _context.Trucks.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Truck GetTruckById(int truckId)
        {
            return _context.Trucks.Find(truckId);
        }

        public List<Truck> GetTrucks()
        {
            List<Truck> trucks = _context.Trucks.ToList();
            return trucks;
        }

        public List<Truck> GetTrucks(string searchStringCategoryName, string searchStringManufacturerName)
        {
            List<Truck> trucks = _context.Trucks.ToList();

            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringManufacturerName))
            {
                trucks = trucks.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())
                && x.Manufacturer.ManufacturerName.ToLower().Contains(searchStringManufacturerName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                trucks = trucks.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringManufacturerName))
            {
                trucks = trucks.Where(x => x.Manufacturer.ManufacturerName.ToLower().Contains(searchStringManufacturerName.ToLower())).ToList();
            }

            return trucks;

        }

        public bool RemoveById(int TruckId)
        {
            var truck = GetTruckById(TruckId);
            if (truck == default(Truck))
            { return false; }

            _context.Remove(truck);
            return _context.SaveChanges() != 0;
        }

        public bool Update(int truckId, int manufacturerId,string model, int categoryId, string image, int year, string engine, int loadcapacity, int quantity, decimal price,string description, decimal discount)
        {
            var truck = GetTruckById(truckId);
            if (truck == default(Truck))
            { return false; }
           // product.TruckName = name;

            //truck.ManufacturerId = manufacturerId;
            truck.Model = model;
            //truck.CategoryId = categoryId;

            truck.Manufacturer = _context.Manufacturers.Find(manufacturerId);
            truck.Category = _context.Categories.Find(categoryId);

            truck.Image = image;
            truck.Year = year;
            truck.Engine = engine;
            truck.Loadcapacity = loadcapacity;
            truck.Quantity = quantity;
            truck.Price = price;
            truck.Description= description;
            truck.Discount = discount;

            _context.Update(truck);
            return _context.SaveChanges() != 0;
        }
    }
}
