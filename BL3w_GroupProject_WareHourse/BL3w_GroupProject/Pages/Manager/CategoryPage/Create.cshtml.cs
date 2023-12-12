using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Manager.CategoryPage
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _context;

        public CreateModel(ICategoryService context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "manager")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "manager")
            {
                return RedirectToPage("/Login");
            }
            try
            {
                Category.CategoryCode = Category.CategoryCode?.ToUpperInvariant();
                _context.AddCategory(Category);
            }
            catch
            {
                ViewData["Error"] = "Product code already exists!";
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
