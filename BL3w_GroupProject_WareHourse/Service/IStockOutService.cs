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
    }
}
