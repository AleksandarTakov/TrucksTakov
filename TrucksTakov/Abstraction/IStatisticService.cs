using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TrucksTakov.Abstraction

{ 
        public interface IStatisticsService
        {
            int CountTrucks();
            int CountClients();
            int CountOrders();
            decimal SumOrders();
        }
    }

