using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Manager
{
    public class ManagerProductListModel : PageModel
    {
        private readonly IAccountService _context;

        public ManagerProductListModel(IAccountService context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Check if the user is a manager based on the session
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
    }
}
