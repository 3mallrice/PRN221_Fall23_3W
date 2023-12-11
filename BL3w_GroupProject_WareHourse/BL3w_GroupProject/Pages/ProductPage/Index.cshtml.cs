using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly IProductService productService;

        public IndexModel()
        {
            productService = new ProductService();
        }

        public IList<Product> Product { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            Product = productService.GetProducts();
            return Page();
        }
    }
}
