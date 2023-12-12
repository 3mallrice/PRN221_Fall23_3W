using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BL3w_GroupProject.Pages.Admin.ManageProduct
{
    public class ListProductModel : PageModel
    {
        private readonly IProductService productService;
        private const int PageSize = 5;
        public ListProductModel()
        {
            productService = new ProductService();
        }

        public IList<Product> Product { get;set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        [BindProperty] public string? SearchBy { get; set; }
        [BindProperty] public string? Keyword { get; set; }
        public IActionResult OnGet(int? pageIndex)
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "admin")
            {
                return RedirectToPage("/Login");
            }
            var productList = productService.GetProducts();
            PageIndex = pageIndex ?? 1;
            var count = productList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = productList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            Product = items;
            return Page();
        }

        public async Task OnPost(int? pageIndex)
        {
            if (Keyword == null)
            {
                OnGet(pageIndex);
            }
            else
            {
                if (SearchBy.Equals("ProductCode"))
                {
                    Product = productService.GetProducts().Where(a => a.ProductCode.ToUpper().Contains(Keyword.Trim().ToUpper())).ToList();
                }
                else if (SearchBy.Equals("Name"))
                {
                    Product = productService.GetProducts().Where(a => a.Name.ToLower().Contains(Keyword.Trim().ToLower())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
