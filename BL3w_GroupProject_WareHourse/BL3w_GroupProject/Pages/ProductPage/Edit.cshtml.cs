using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.ProductPage
{
    public class EditModel : PageModel
    {
        private readonly IProductService productService;
        private readonly IStorageService storageService;
        private readonly ICategoryService categoryService;

        public EditModel()
        {
            productService = new ProductService();
            storageService = new StorageService();
            categoryService = new CategoryService();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  productService.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                Product = productService.UpdateProduct(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int? id)
        {
          return productService.GetProductByID(id) != null;
        }
    }
}
