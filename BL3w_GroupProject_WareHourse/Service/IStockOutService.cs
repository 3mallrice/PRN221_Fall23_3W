using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStockOutService
    {
        List<StockOut> GetStockOuts();
        StockOut GetStockOutById(int id);
        bool AddStockOut(StockOut stockOut);
        bool AddStockOutDetail(int stockOutId, List<StockOutDetail> stockOutDetails);
        List<StockOutDetail> GetStockOutsDetail();
        List<StockOutDetail> GetStockOutDetailById(int id);
        void UpdateStockOuts(StockOut stockOut);
        void UpdateStockOutsDetail(int stockOutDetailsId, int Quantity);
    }
}
