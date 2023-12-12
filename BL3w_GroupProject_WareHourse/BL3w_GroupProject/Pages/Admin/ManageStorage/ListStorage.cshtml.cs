using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Admin.ManageStorage
{
    public class ListStorageModel : PageModel
    {
        private readonly IStorageService storageService;
        private const int PageSize = 5;
        public ListStorageModel()
        {
            storageService = new StorageService();
        }

        public IList<StorageArea> StorageArea { get;set; } = default!;
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
            var storageAreasList = storageService.GetStorageAreas();
            PageIndex = pageIndex ?? 1;
            var count = storageAreasList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = storageAreasList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            StorageArea = items;
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
                if (SearchBy.Equals("AreaCode"))
                {
                    StorageArea = storageService.GetStorageAreas().Where(a => a.AreaCode.ToUpper().Contains(Keyword.Trim().ToUpper())).ToList();
                }
                else if (SearchBy.Equals("AreaName"))
                {
                    StorageArea = storageService.GetStorageAreas().Where(a => a.AreaName.ToLower().Contains(Keyword.Trim().ToLower())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}
