using BusinessObject.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private CategoryDAO categoryDAO;

        public CategoryRepo()
        {
            categoryDAO = new CategoryDAO();
        }

        public List<Category> GetCategories()
        {
            return categoryDAO.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return categoryDAO.GetCategoryAreaByID(id);
        }

        public void AddCategory(Category category)
        {
            categoryDAO.AddCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            return categoryDAO.UpdateCategory(category);
        }

        public bool ToggleCategoryStatus(int categoryId)
        {
            return categoryDAO.ToggleCategoryStatus(categoryId);
        }
    }
}
