using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly IProductService productService;
        private readonly IStorageService storageService;
        private readonly ICategoryService categoryService;


        public CreateModel()
        {
            productService = new ProductService();
            storageService = new StorageService();
            categoryService = new CategoryService();
        }

        public IActionResult OnGet()
        {
            var StorageAreasTypeList = storageService.LoadArea();
            var StorageAreasSelectList = StorageAreasTypeList.Select(area => new SelectListItem
            {
                Value = area.AreaId.ToString(),
                Text = area.AreaName.ToString(),
            }).ToList();
            var CategoriesTypeList = categoryService.LoadCategories();
            var CategoriesSelectList = CategoriesTypeList.Select(category => new SelectListItem
            {
                Value = category.CategoryId.ToString(),
                Text = category.Name
            }).ToList();
            ViewData["AreaId"] = new SelectList(StorageAreasSelectList, "Value", "Text");
            ViewData["CategoryId"] = new SelectList(CategoriesSelectList, "Value", "Text");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Product = productService.AddProduct(Product);
            return RedirectToPage("./Index");
        }
    }
}
