using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace BL3w_GroupProject.Pages.Manager.StoragePage
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext _context;

        public CreateModel(BusinessObject.Models.PRN221_Fall23_3W_WareHouseManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StorageArea StorageArea { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.StorageAreas == null || StorageArea == null)
            {
                return Page();
            }

            _context.StorageAreas.Add(StorageArea);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
