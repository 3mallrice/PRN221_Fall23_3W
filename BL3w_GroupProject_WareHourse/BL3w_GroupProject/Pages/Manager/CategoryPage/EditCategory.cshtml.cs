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

namespace BL3w_GroupProject.Pages.Manager.CategoryPage
{
    public class EditCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public EditCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGetEdit(int id)
        {
            if (!IsUserAuthorized())
            {
                return RedirectToPage("/Login");
            }

            var category = _categoryService.GetCategoryById(id);

            if (Category == null)
            {
                return NotFound();
            }
            Category = category;
            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!IsUserAuthorized())
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "Name");
                return Page();
            }

            _categoryService.UpdateCategory(Category);

            return RedirectToPage("./ListCategory");
        }

        private bool IsUserAuthorized()
        {
            var account = HttpContext.Session.GetString("account");

            return !string.IsNullOrEmpty(account) && account == "manager";
        }
    }
}
