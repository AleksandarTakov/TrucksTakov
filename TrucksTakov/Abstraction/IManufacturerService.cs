using System.Collections.Generic;
using TrucksTakov.Domain;

namespace TrucksTakov.Abstraction
{
    public interface IManufacturerService
    {

            List<Manufacturer> GetManufacturers();
            Manufacturer GetManufacturerById(int manufacturerId);
            List<Truck> GetTruckByManufacturer(int manufacturerId);
    }
}