using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrucksTakov.Models.Category;
using TrucksTakov.Models.Truck;
using TrucksTakov.Abstraction;
using TrucksTakov.Models.Manufacturer;
using TrucksTakov.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using TrucksTakov.Services;

namespace TrucksTakov.Controllers
{
    [Authorize(Roles = "Administrator")]

    public class TruckController : Controller
    {
        private readonly ITruckService _truckService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;

        public TruckController(ITruckService truckService, ICategoryService categoryService, IManufacturerService manufacturerService)
        {
            this._truckService = truckService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
        }

        public ActionResult Create()
        {
            var truck = new TruckCreateVM();
            truck.Manufacturers = _manufacturerService.GetManufacturers()
                .Select(x => new ManufacturerPairVM()
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();
            truck.Categories = _categoryService.GetCategories()
                .Select(x => new CategoryPairVM()
                {
                    Id = x.Id,
                    Name = x.CategoryName
                }).ToList();
            return View(truck);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] TruckCreateVM truck)
        {
            if (ModelState.IsValid)
            {
                var createdId = _truckService.Create(truck.ManufacturerId, truck.Model, truck.CategoryId, truck.Image,
                    truck.Year, truck.Engine, truck.Loadcapacity,
                    truck.Quantity, truck.Price, truck.Description, truck.Discount);

                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringManufacturerName)
        {
            List<TruckIndexVM> trucks = _truckService.GetTrucks(searchStringCategoryName, searchStringManufacturerName)
                .Select(truck => new TruckIndexVM
                {
                    Id = truck.Id,
                    //  TruckName = trucks.TruckName,
                    ManufacturerId = truck.ManufacturerId,
                    ManufacturerName = truck.Manufacturer.ManufacturerName,
                    Model = truck.Model,
                    CategoryId = truck.CategoryId,
                    CategoryName = truck.Category.CategoryName,
                    Image = truck.Image,
                    Year = truck.Year,
                    Engine = truck.Engine,
                    Loadcapacity = truck.Loadcapacity,
                    Quantity = truck.Quantity,
                    Price = truck.Price,
                    Description = truck.Description,
                    Discount = truck.Discount
                }).ToList();
            return this.View(trucks);
        }
        [AllowAnonymous]
        public ActionResult Tug()
        { 
            List<TruckIndexVM> trucks = _truckService.GetTrucks()
            .Select(truck => new TruckIndexVM()
            {
                Id = truck.Id,
                ManufacturerId = truck.ManufacturerId,
                ManufacturerName = truck.Manufacturer.ManufacturerName,
                CategoryId = truck.CategoryId,
                CategoryName = truck.Category.CategoryName,
                Image = truck.Image,
                Year = truck.Year,
                Engine = truck.Engine,
                Loadcapacity = truck.Loadcapacity,
                Quantity = truck.Quantity,
                Price = truck.Price,
                Description = truck.Description,
                Discount = truck.Discount

            }).Where(x => x.CategoryName == "Tug").ToList();

            return this.View(trucks);
        }
        public ActionResult Edit(int id)
        {
            Truck truck = _truckService.GetTruckById(id);
            if (truck == null)
            {
                return NotFound();
            }
            TruckEditVM updatedTruck = new TruckEditVM()
            {
                Id = truck.Id,
                ManufacturerId = truck.ManufacturerId,
                Model = truck.Model,
                CategoryId = truck.CategoryId,
                Image = truck.Image,
                Year = truck.Year,
                Engine = truck.Engine,
                Loadcapacity = truck.Loadcapacity,
                Quantity = truck.Quantity,
                Price = truck.Price,
                Description = truck.Description,
                Discount = truck.Discount
            };
            updatedTruck.Manufacturers = _manufacturerService.GetManufacturers()
                .Select(b => new ManufacturerPairVM()
                {
                    Id = b.Id,
                    Name = b.ManufacturerName
                })
                .ToList();
            updatedTruck.Categories = _categoryService.GetCategories()
                .Select(c => new CategoryPairVM()
                {
                    Id = c.Id,
                    Name = c.CategoryName
                })
                .ToList();
            return View(updatedTruck);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TruckEditVM truck)
        {
            {
                if (ModelState.IsValid)
                {
                    var updated = _truckService.Update(id, truck.ManufacturerId, truck.Model, truck.CategoryId, truck.Image, truck.Year, truck.Engine, truck.Loadcapacity, truck.Quantity, truck.Price, truck.Description, truck.Discount);

                    if (updated)
                    {
                        return this.RedirectToAction("Index");
                    }
                }
                return View(truck);
            }
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Truck item = _truckService.GetTruckById(id);
            if (item == null)
            {
                return NotFound();
            }

            TruckDetailsVM truck = new TruckDetailsVM()
            {
                Id = item.Id,
                ManufacturerId = item.ManufacturerId,
                ManufacturerName = item.Manufacturer.ManufacturerName,
                Model = item.Model,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Image = item.Image,
                Year = item.Year,
                Engine = item.Engine,
                Loadcapacity = item.Loadcapacity,
                Quantity = item.Quantity,
                Price = item.Price,
                Description = item.Description,
                Discount = item.Discount
            };
            return View(truck);
        }



        public ActionResult Delete(int id)
        {
            Truck item = _truckService.GetTruckById(id);
            if (item == null)
            {
                return NotFound();

            }
            TruckDeleteVM truck = new TruckDeleteVM()
            {
                Id = item.Id,
                //  TruckName = trucks.TruckName,

                ManufacturerId = item.ManufacturerId,
                ManufacturerName = item.Manufacturer.ManufacturerName,
                Model = item.Model,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Image = item.Image,
                Year = item.Year,
                Engine = item.Engine,
                Loadcapacity = item.Loadcapacity,
                Quantity = item.Quantity,
                Price = item.Price,
                Description = item.Description,
                Discount = item.Discount
            };
            return this.View(truck);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _truckService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
        
        
