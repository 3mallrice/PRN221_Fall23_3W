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
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    stockOuts = db.StockOuts
                    .Include(x => x.StockOutDetails)
                    .Include(x => x.Account)
                    .Include(x => x.Partner)
                    .OrderByDescending(x => x.Status)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stockOuts;
        }
        public List<StockOutDetail> GetStockOutsDetail()
        {
            List<StockOutDetail> stockOutsDetail = null;
            try
            {
                using (var dbContext = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    stockOutsDetail = dbContext.StockOutDetails
                        .Include(x => x.Product)
                        .Include(x => x.StockOut)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stockOutsDetail;
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
                using (var dbContext = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    stockOut.DateOut = DateTime.Now;
                    stockOut.Status = 1;
                    dbContext.StockOuts.Add(stockOut);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool AddStockOutDetail(int stockOutId, List<StockOutDetail> stockOutDetails)
        {
            using var transaction = dbContext.Database.BeginTransaction();

            try
            {
                using (var dbContext = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    var stockOut = dbContext.StockOuts.Find(stockOutId);

                    if (stockOut == null)
                    {
                        transaction.Rollback();
                        throw new Exception($"StockOut with ID {stockOutId} not found.");
                    }

                    foreach (var detail in stockOutDetails)
                    {
                        var product = dbContext.Products.Find(detail.ProductId);

                        if (product == null || product.Quantity < detail.Quantity)
                        {
                            transaction.Rollback();
                            throw new Exception("Invalid product or insufficient quantity.");
                        }

                        product.Quantity -= detail.Quantity;

                        detail.StockOutId = stockOutId;
                        dbContext.StockOutDetails.Add(detail);
                    }

                    dbContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                transaction.Rollback();
                return false;
            }
        }

        public List<StockOutDetail> GetStockOutDetailById(int id)
        {
            List<StockOutDetail> stockOutDetail = null;

            try
            {
                stockOutDetail = dbContext.StockOutDetails
                    .Include(x => x.StockOut)
                    .Include(x => x.Product)
                    .Where(x => x.StockOutId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stockOutDetail;
        }

        public void UpdateStockOuts(StockOut stockOut)
        {
            var _dbContext = new PRN221_Fall23_3W_WareHouseManagementContext();
            try
            {
                stockOut.DateOut = DateTime.Now;
                stockOut.Status = 1;
                _dbContext.StockOuts.Update(stockOut);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStockOutsDetail(List<StockOutDetail> stockOutDetails, int stockOutId)
        {
            var _dbContext = new PRN221_Fall23_3W_WareHouseManagementContext();
            try
            {
                List<StockOutDetail> stockOutDetailsList = GetStockOutDetailById(stockOutId);

                foreach (var detail in stockOutDetailsList)
                {
                    var existingDetail = stockOutDetails.FirstOrDefault(d => d.StockOutDetailId == detail.StockOutDetailId);

                    if (existingDetail != null)
                    {
                        _dbContext.Entry(detail).CurrentValues.SetValues(existingDetail);
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
