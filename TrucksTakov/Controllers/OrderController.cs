using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrucksTakov.Data;
using TrucksTakov.Domain;
using TrucksTakov.Models.Order;

namespace TrucksTakov.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            this._context = context;
        }
        // GET: OrderController

        public ActionResult Index()
        {

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            List<OrderIndexVM> orders = _context.Orders.Select(x => new OrderIndexVM
            {
                Id = x.Id,
                OrderDate = x.OrderDate.ToString("dd-MMM,yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = x.UserId,
                User = x.User.UserName,
                TruckId = x.TruckId,
                Model= x.Truck.Model,
                Image =x.Truck.Image,
                Quantity = x.Quantity,
                Price = x.Price,
                Description=x.Truck.Description,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
            }).ToList();
            return View(orders);
        }
        [AllowAnonymous]
        public IActionResult MyOrders(decimal searchPrice)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.SingleOrDefault(u => u.Id == currentUserId);
            if (user == null)
            {
                return null;
            }
            List<OrderIndexVM> orders = _context.Orders.Where(x => x.UserId == user.Id).Select(x => new OrderIndexVM
            {
                Id = x.Id,
                OrderDate = x.OrderDate.ToString("dd-MMM,yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = x.UserId,
                User = x.User.UserName,
                TruckId = x.TruckId,
                Model = x.Truck.Model,
                Image = x.Truck.Image,
                Quantity = x.Quantity,
                Price = x.Price,
                Description = x.Truck.Description,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
            }).ToList();
            if(searchPrice !=0)
            {
                orders = orders.Where(x => x.Price == searchPrice).ToList();
            }
            return this.View(orders);

        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        [AllowAnonymous]
        public ActionResult Create(int truckId, int quantity)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            var truck = this._context.Trucks.SingleOrDefault(x => x.Id == truckId);

            if (user == null || truck == null || truck.Quantity < quantity)
            {
                return this.RedirectToAction("Index", "Truck");
            }
            OrderConfirmVM orderForDb = new OrderConfirmVM
            {
                UserId = userId,
                User = user.UserName,
                TruckId = truckId,
                Model=truck.Model,
                Image = truck.Image,
                Quantity = quantity,
                Price = truck.Price,
                Description= truck.Description,
                Discount = truck.Discount,
                TotalPrice = quantity * truck.Price - quantity * truck.Price * truck.Discount / 100
            };
            return View(orderForDb);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create(OrderConfirmVM bindingModel)
        {
            if (ModelState.IsValid)
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.SingleOrDefault(u => u.Id == userId);
                var truck = this._context.Trucks.SingleOrDefault(x => x.Id == bindingModel.TruckId);

                if (user == null || truck == null || bindingModel.Quantity < bindingModel.Quantity || bindingModel.Quantity == 0)
                {
                    return this.RedirectToAction("Index", "Truck");
                }
                Order orderForDb = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    TruckId = bindingModel.TruckId,
                    UserId = userId,
                    Quantity = bindingModel.Quantity,
                    Price = truck.Price,
                    Discount = truck.Discount,
                };
                truck.Quantity -= bindingModel.Quantity;
                this._context.Trucks.Update(truck);
                this._context.Orders.Add(orderForDb);
                this._context.SaveChanges();
            }
            return this.RedirectToAction("Index", "Truck");
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

