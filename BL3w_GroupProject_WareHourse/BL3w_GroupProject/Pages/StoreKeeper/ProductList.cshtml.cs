using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.StoreKeeper
{
    public class ProductListModel : PageModel
    {
        private readonly IProductService productService;

        public ProductListModel()
        {
            productService = new ProductService();
        }

        public IList<Product> Product { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Index");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "storekeeper")
            {
                return RedirectToPage("/Index");
            }
            
            Product = productService.GetProducts();
            return Page();
        }
    }
}
