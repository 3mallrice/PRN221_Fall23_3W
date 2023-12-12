using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;

        public CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> category;
            try
            {
                var context = new PRN221_Fall23_3W_WareHouseManagementContext();
                category = context.Categories
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public Category GetCategoryAreaByID(int id)
        {
            Category category = null;
            try
            {
                var db = new PRN221_Fall23_3W_WareHouseManagementContext();
                category = db.Categories.SingleOrDefault(u => u.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public void AddCategory(Category category)
        {
            try
            {
                bool existingCategory = GetCategories()
                    .Any(a => a.CategoryCode.ToLower().Equals(category.CategoryCode.ToLower()));

                if (existingCategory == false)
                {
                    category.Status = 1;

                    using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                    {
                        db.Categories.Add(category);
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Category already exists!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Add Category: {ex.Message}", ex);
            }
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    var existing = db.Categories.SingleOrDefault(x => x.CategoryId == category.CategoryId);
                    if (existing != null)
                    {
                        existing.CategoryCode = category.CategoryCode;
                        existing.Name = category.Name;
                        existing.Status = category.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Category not found for updating.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateCategory: {ex.Message}", ex);
                return false;
            }
        }

        public bool ToggleCategoryStatus(int categoryId)
        {
            try
            {
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    var existing = db.Products.SingleOrDefault(x => x.CategoryId == categoryId);
                    if (existing != null)
                    {
                        existing.Status = (existing.Status == 1) ? 0 : 1;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Category not found for status toggling.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ToggleCategoryStatus: {ex.Message}", ex);
                return false;
            }
        }

        public List<Category> LoadCategories()
        {
            List<Category> loadArea;
            try
            {
                var myStoreDB = new PRN221_Fall23_3W_WareHouseManagementContext();
                loadArea = myStoreDB.Categories
               .Where(area => area.CategoryId != null)
               .Select(area => new Category
               {
                   CategoryId = area.CategoryId,
                   Name = area.Name
               })
               .Distinct()
               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return loadArea;
        }
    }
}
