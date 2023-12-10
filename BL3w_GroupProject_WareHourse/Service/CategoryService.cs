using BusinessObject.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepo categoryRepository;

        public CategoryService()
        {
            categoryRepository = new CategoryRepo();
        }

        public List<Category> GetCategories()
        {
            return categoryRepository.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return categoryRepository.GetCategoryById(id);
        }

        public void AddCategory(Category category)
        {
            categoryRepository.AddCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            return categoryRepository.UpdateCategory(category);
        }

        public bool ToggleCategoryStatus(int categoryId)
        {
            return categoryRepository.ToggleCategoryStatus(categoryId);
        }
    }
}
