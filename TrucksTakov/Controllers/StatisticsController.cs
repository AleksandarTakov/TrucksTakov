﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrucksTakov.Models.Statistics;
using TrucksTakov.Abstraction;


namespace TrucksTakov.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }
        public IActionResult Index()
        {
            StatisticsVM statistics = new StatisticsVM();

            statistics.CountClients = statisticsService.CountClients();
            statistics.CountTrucks = statisticsService.CountTrucks();
            statistics.CountOrders = statisticsService.CountOrders();
            statistics.SumOrders = statisticsService.SumOrders();

            return View(statistics);
        }
    }
}
