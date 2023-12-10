using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.PartnerPage
{
    public class DeleteModel : PageModel
    {
        private readonly IPartnerService _context;

        public DeleteModel(IPartnerService context)
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
            else 
            {
                Partner = partner;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partner = _context.GetPartnerByID(id);

            if (partner != null)
            {
                partner.Status = 0;
                _context.UpdatePartner(partner);
            }

            return RedirectToPage("./Index");
        }
    }
}
