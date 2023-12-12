using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Admin.ManageStockOutDetail
{
    public class ListStockOutDetailModel : PageModel
    {
        private readonly IStockOutService stockOutService;
        private const int PageSize = 5;

        public ListStockOutDetailModel()
        {
            stockOutService = new StockOutService();
        }

        public IList<StockOutDetail> StockOutDetail { get;set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        [BindProperty] public string? SearchBy { get; set; }
        [BindProperty] public int? Keyword { get; set; }

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
            var stockOutList = stockOutService.GetStockOutsDetail();
            PageIndex = pageIndex ?? 1;
            var count = stockOutList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = stockOutList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            StockOutDetail = items;
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
                if (SearchBy.Equals("ProductId"))
                {
                    StockOutDetail = stockOutService.GetStockOutsDetail().Where(a => a.ProductId == Keyword).ToList();
                }
                else if (SearchBy.Equals("StockOutId"))
                {
                    StockOutDetail = stockOutService.GetStockOutsDetail().Where(a => a.StockOutId == Keyword).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
