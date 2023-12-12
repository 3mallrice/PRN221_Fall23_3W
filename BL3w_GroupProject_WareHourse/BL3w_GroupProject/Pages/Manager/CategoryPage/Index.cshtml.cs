using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace BL3w_GroupProject.Pages.Manager.CategoryPage
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext _context;

        public IndexModel(BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
        }
    }
}
