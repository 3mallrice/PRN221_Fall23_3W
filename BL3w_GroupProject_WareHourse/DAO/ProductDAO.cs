using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;

        public ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return Instance;
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> listProduct = null;
            try
            {
                var dbContext = new PRN221_Fall23_3W_WareHouseManagementContext();
                listProduct = dbContext.Products
                    .Include(c => c.Area)
                    .Include(c => c.Category)
                    .Include(c => c.StockOutDetails)
                    .Include(c => c.LotDetails)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProduct;
        }



        public Product AddProduct(Product product)
        {
            try
            {
                var myStoreDB = new PRN221_Fall23_3W_WareHouseManagementContext();
                myStoreDB.Products.Add(product);
                myStoreDB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public Product GetProductByID(int? id)
        {
            Product product = null;
            try
            {
                var myStoreDB = new PRN221_Fall23_3W_WareHouseManagementContext();
                product = myStoreDB.Products
                            .AsNoTracking()
                            .Include(c => c.Area)
                            .Include(c => c.Category)
                            .Include(c => c.StockOutDetails)
                            .Include(c => c.LotDetails)
                            .SingleOrDefault(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            try
            {
                var myStoreDB = new PRN221_Fall23_3W_WareHouseManagementContext();
                myStoreDB.Products.Update(product);
                myStoreDB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public Product DeleteProduct(int? productId)
        {
            Product product = GetProductByID(productId);
            try
            {
                var myStoreDB = new PRN221_Fall23_3W_WareHouseManagementContext();
                myStoreDB.Products.Remove(product);
                myStoreDB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
    }
}
