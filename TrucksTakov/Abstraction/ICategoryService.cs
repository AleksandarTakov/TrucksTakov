using System.Collections.Generic;
using TrucksTakov.Domain;

namespace TrucksTakov.Abstraction
{
    public interface ICategoryService
    {
            List<Category> GetCategories();
            Category GetCategoryById(int categoryId);
            List<Truck> GetTrucksByCategory(int categoryId);

    }
}
