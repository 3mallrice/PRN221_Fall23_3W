using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace BL3w_GroupProject.Pages.Manager.StoragePage
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext _context;

        public DetailsModel(BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext context)
        {
            _context = context;
        }

      public StorageArea StorageArea { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StorageAreas == null)
            {
                return NotFound();
            }

            var storagearea = await _context.StorageAreas.FirstOrDefaultAsync(m => m.AreaId == id);
            if (storagearea == null)
            {
                return NotFound();
            }
            else 
            {
                StorageArea = storagearea;
            }
            return Page();
        }
    }
}
