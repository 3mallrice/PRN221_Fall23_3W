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
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext _context;

        public DeleteModel(BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.StorageAreas == null)
            {
                return NotFound();
            }
            var storagearea = await _context.StorageAreas.FindAsync(id);

            if (storagearea != null)
            {
                StorageArea = storagearea;
                _context.StorageAreas.Remove(StorageArea);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
