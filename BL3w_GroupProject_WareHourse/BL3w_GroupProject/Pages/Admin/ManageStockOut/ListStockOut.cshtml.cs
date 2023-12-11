using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using System.Drawing.Printing;

namespace BL3w_GroupProject.Pages.Admin.ManageStockOut
{
    public class ListStockOutModel : PageModel
    {
        private readonly IStockOutService stockOutService;
        private const int PageSize = 5;
        public ListStockOutModel()
        {
            stockOutService = new StockOutService();
        }

        public IList<StockOut> StockOut { get;set; } = default!;
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
            var stockOutList = stockOutService.GetStockOuts();
            PageIndex = pageIndex ?? 1;
            var count = stockOutList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = stockOutList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            StockOut = items;
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
                if (SearchBy.Equals("AccountCode"))
                {
                    StockOut = stockOutService.GetStockOuts().Where(a => a.Account.AccountCode.ToUpper().Contains(Keyword.Trim().ToUpper())).ToList();
                }
                else if (SearchBy.Equals("PartnerName"))
                {
                    StockOut = stockOutService.GetStockOuts().Where(a => a.Partner.Name.ToLower().Contains(Keyword.Trim().ToLower())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
