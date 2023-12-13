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

        public List<StockOutDetail> GetStockOutsDetail() => stockOutRepo.GetStockOutsDetail();

        public List<StockOutDetail> GetStockOutDetailById(int id) => stockOutRepo.GetStockOutDetailById(id);

        public void UpdateStockOuts(StockOut stockOut) => stockOutRepo.UpdateStockOuts(stockOut);

<<<<<<< HEAD
        public void UpdateStockOutsDetail(int stockOutDetailsId, int Quantity) => stockOutRepo.UpdateStockOutsDetail(stockOutDetailsId, Quantity);
=======
        public void UpdateStockOutsDetail(List<StockOutDetail> stockOutDetails, int stockOutId) => stockOutRepo.UpdateStockOutsDetail(stockOutDetails, stockOutId);

        public bool AddStockOutDetail(int stockOutId, List<StockOutDetail> stockOutDetails) => stockOutRepo.AddStockOutDetail(stockOutId, stockOutDetails);
>>>>>>> 8ef67e13d9899f5aee60d9750f1bbaf6dc2633fc
    }
}
