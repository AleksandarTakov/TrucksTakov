using System.Collections.Generic;
using TrucksTakov.Data;
using TrucksTakov.Domain;
using System.Linq;
using TrucksTakov.Abstraction;

namespace TrucksTakov.Services
{

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }
        public List<Truck> GetTrucksByCategory(int categoryId)
        {
            return _context.Trucks
                .Where(x => x.CategoryId == categoryId)
                .ToList();
        }
    }
}
