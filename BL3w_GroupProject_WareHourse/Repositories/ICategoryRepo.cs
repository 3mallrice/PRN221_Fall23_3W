using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICategoryRepo
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        bool UpdateCategory(Category category);
        bool ToggleCategoryStatus(int categoryId);
        List<Category> LoadCategories();
    }
}
