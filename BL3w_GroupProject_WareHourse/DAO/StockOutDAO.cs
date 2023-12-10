using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class StockOutDAO
    {
        private static StockOutDAO instance = null;
        private static PRN221_Fall23_3W_WareHouseManagementContext dbContext = new PRN221_Fall23_3W_WareHouseManagementContext();
        public StockOutDAO() { }
        public static StockOutDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StockOutDAO();
                }
                return instance;
            }
        }

        public List<StockOut> GetStockOuts()
        {
            List<StockOut> stockOuts = null;
            try
            {
                stockOuts = dbContext.StockOuts
                    .Include(x => x.StockOutDetails)
                    .Include(x => x.Account)
                    .Include(x => x.Partner)
                    .OrderByDescending(x => x.Status)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stockOuts;
        }

        public StockOut GetStockOutById(int id)
        {
            StockOut stockOut = null;

            try
            {
                stockOut = dbContext.StockOuts
                    .Include(x => x.StockOutDetails)
                    .Include(x => x.Account)
                    .Include(x => x.Partner)
                    .SingleOrDefault(x => x.StockOutId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stockOut;
        }

        public bool AddStockOut(StockOut stockOut)
        {
            try
            {
                stockOut.DateOut = new DateTime();
                dbContext.StockOuts.Add(stockOut);
                dbContext.SaveChanges();

                foreach (var stockOutDetail in stockOut.StockOutDetails)
                {
                    stockOutDetail.StockOutId = stockOut.StockOutId;
                    dbContext.StockOutDetails.Add(stockOutDetail);
                }

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding StockOut: {ex.Message}");
                return false;
            }
        }
    }
}
