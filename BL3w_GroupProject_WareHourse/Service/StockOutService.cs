using BusinessObject.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StockOutService : IStockOutService
    {
        private IStockOutRepo stockOutRepo = null;

        public StockOutService()
        {
            stockOutRepo = new StockOutRepo();
        }
        public bool AddStockOut(StockOut stockOut) => stockOutRepo.AddStockOut(stockOut);

        public StockOut GetStockOutById(int id) => stockOutRepo.GetStockOutById(id);

        public List<StockOut> GetStockOuts() => stockOutRepo.GetStockOuts();
    }
}
