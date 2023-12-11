using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Admin.ManageLot
{
    public class ListLotModel : PageModel
    {
        private readonly ILotService lotService;
        private const int PageSize = 5;

        public ListLotModel()
        {
            lotService = new LotService();
        }

        public IList<Lot> Lot { get;set; } = default!;
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
            var LotList = lotService.GetAllLots().ToList();
            PageIndex = pageIndex ?? 1;
            var count = LotList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = LotList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Lot = items;
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
                    Lot = lotService.GetAllLots().Where(a => a.LotCode.ToUpper().Contains(Keyword.Trim().ToUpper())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
