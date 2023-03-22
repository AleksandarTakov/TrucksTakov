using System.Collections.Generic;
using TrucksTakov.Domain;

namespace TrucksTakov.Abstraction
{
    public interface ITruckService
    { 
            bool Create( int manufacturerdId,string model, int categoryId, string image, int year, string engine, int loadcapacity, int quantity, decimal price,string description, decimal discount);
            bool Update(int truckId, int manufacturerId,string model, int categoryId, string image,int year, string engine, int loadcapacity, int quantity, decimal price,string description, decimal discount);
            List<Truck> GetTrucks();
            Truck GetTruckById(int truckId);
            bool RemoveById(int truckId);
            List<Truck> GetTrucks( string searchStringCategoryName,string searchStringManufacturerName);
    
    }
    }

