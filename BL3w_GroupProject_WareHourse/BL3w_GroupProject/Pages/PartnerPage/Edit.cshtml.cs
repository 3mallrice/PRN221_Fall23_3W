using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.PartnerPage
{
    public class EditModel : PageModel
    {
        private readonly IPartnerService _context;

        public EditModel(IPartnerService context)
        {
            _context = context;
        }

        [BindProperty]
        public Partner Partner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner =  _context.GetPartnerByID(id);
            if (partner == null)
            {
                return NotFound();
            }
            Partner = partner;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.UpdatePartner(Partner);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerExists(Partner.PartnerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PartnerExists(int id)
        {
          return (_context.GetPartnerByID(id)) != null;
        }
    }
}
