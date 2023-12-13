using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public List<StockOutDetail> GetStockOutsDetail()
        {
            List<StockOutDetail> stockOutsDetail = null;
            try
            {
                stockOutsDetail = dbContext.StockOutDetails
                    .Include(x => x.Product)
                    .Include(x => x.StockOut)
                    .ToList();
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

        public void UpdateStockOuts(StockOut NewstockOut)
        {
            var _dbContext = new PRN221_Fall23_3W_WareHouseManagementContext();
            try
            {
                StockOut OldStockOut = GetStockOutById(NewstockOut.StockOutId);

                // Export Json StockOut 
                var jsonFormatContent = JsonConvert.SerializeObject(OldStockOut, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                string fileName = @"D:\StockOuts.txt";
                if (System.IO.File.Exists(fileName) == false)
                {
                    System.IO.File.WriteAllText(fileName, DateTime.Now + "\n" + jsonFormatContent + "\n");
                }
                else
                {
                    System.IO.File.AppendAllText(fileName, jsonFormatContent);
                }

                NewstockOut.DateOut = DateTime.Now;
                NewstockOut.Status = 1;
                _dbContext.StockOuts.Update(NewstockOut);
                _dbContext.SaveChanges();

                // Export Json StockOut Update
                var jsonFormatContent2 = JsonConvert.SerializeObject(NewstockOut, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                string fileName2 = @"D:\StockOutsUpdate.txt";

                if (System.IO.File.Exists(fileName2) == false)
                {
                    System.IO.File.WriteAllText(fileName2, DateTime.Now + "\n" + jsonFormatContent2 + "\n");
                }
                else
                {
                    System.IO.File.AppendAllText(fileName2, DateTime.Now + "\n" + jsonFormatContent2 + "\n");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStockOutsDetail(int stockOutDetailsId, int Quantity)
        {
                var _dbContext = new PRN221_Fall23_3W_WareHouseManagementContext();
                try
                {
                    StockOutDetail stockOutDetailsList = _dbContext.StockOutDetails
                        .FirstOrDefault(s => s.StockOutDetailId == stockOutDetailsId);

                    if (stockOutDetailsList != null)
                    {
                        // Export Json StockOutDetail
                        var jsonFormatContent = JsonConvert.SerializeObject(stockOutDetailsList, Formatting.None, new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                        string fileName = @"D:\StockOutsDetail.txt";
                        using (StreamWriter streamWriter = new StreamWriter(fileName, true))
                        {
                            streamWriter.WriteLine(DateTime.Now);
                            streamWriter.WriteLine(jsonFormatContent);
                        }

                        stockOutDetailsList.Quantity = Quantity;
                        _dbContext.Entry(stockOutDetailsList).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        // Export Json StockOutDetail After Update
                        var jsonFormatContent2 = JsonConvert.SerializeObject(stockOutDetailsList, Formatting.None, new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                        string fileName2 = @"D:\StockOutsDetailUpdate.txt";
                        using (StreamWriter streamWriter2 = new StreamWriter(fileName2, true))
                        {
                            streamWriter2.WriteLine(DateTime.Now);
                            streamWriter2.WriteLine(jsonFormatContent2);
                        }
                    }
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StockOutDetail GetStockOutsDetailById(int id)
        {
            StockOutDetail stockOutDetail = null;

            try
            {
                stockOutDetail = dbContext.StockOutDetails
                    .Include(x => x.StockOut)
                    .Include(x => x.Product)
                    .SingleOrDefault(x => x.StockOutId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stockOutDetail;
        }
    }
}
