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

namespace BL3w_GroupProject.Pages.Admin.ManagePartner
{
    public class ListPartnerModel : PageModel
    {
        private readonly IPartnerService partnerService;
        private const int PageSize = 5;
        public ListPartnerModel()
        {
            partnerService = new PartnerService();
        }

        public IList<Partner> Partner { get;set; } = default!;
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
            var PartnerList = partnerService.GetPartners();
            PageIndex = pageIndex ?? 1;
            var count = PartnerList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = PartnerList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            Partner = items;
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
                if (SearchBy.Equals("PartnerCode"))
                {
                    Partner = partnerService.GetPartners().Where(a => a.PartnerCode.ToUpper().Equals(Keyword.ToUpper().Trim())).ToList();
                }
                else if (SearchBy.Equals("PartnerName"))
                {
                    Partner = partnerService.GetPartners().Where(a => a.Name.ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
