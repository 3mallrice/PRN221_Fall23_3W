using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace BL3w_GroupProject.Pages.StoreKeeper
{
    public class ProductListModel : PageModel
    {
        private readonly BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext _context;

        public ProductListModel(BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Area)
                .Include(p => p.Category).ToListAsync();
            }
        }
    }
}
