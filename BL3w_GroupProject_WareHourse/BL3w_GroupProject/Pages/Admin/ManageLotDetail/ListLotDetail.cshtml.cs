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

namespace BL3w_GroupProject.Pages.Admin.ManageLotDetail
{
    public class ListLotDetailModel : PageModel
    {
        private readonly ILotService lotService;
        private const int PageSize = 5;
        public ListLotDetailModel()
        {
            lotService = new LotService();
        }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        [BindProperty] public string? SearchBy { get; set; }
        [BindProperty] public string? Keyword { get; set; }
        public IList<LotDetail> LotDetail { get;set; } = default!;

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
            var LotDetailList = lotService.GetAllLotDetail().ToList();
            PageIndex = pageIndex ?? 1;
            var count = LotDetailList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = LotDetailList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            LotDetail = items;
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
                if (SearchBy.Equals("LotCode"))
                {
                    LotDetail = lotService.GetAllLotDetail().Where(a => a.Lot.LotCode.ToUpper().Equals(Keyword.ToUpper().Trim())).ToList();
                } 
                else if (SearchBy.Equals("ProductCode"))
                {
                    LotDetail = lotService.GetAllLotDetail().Where(a => a.Product.ProductCode.ToUpper().Equals(Keyword.ToUpper().Trim())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
