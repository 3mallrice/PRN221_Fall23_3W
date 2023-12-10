using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.PartnerPage
{
    public class CreateModel : PageModel
    {
        private readonly IPartnerService _context;

        public CreateModel(IPartnerService context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Partner Partner { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Partner == null)
            {
                return Page();
            }
            try
            {
                Partner.PartnerCode = Partner.PartnerCode?.ToUpperInvariant();
                _context.AddPartner(Partner);
            }
            catch
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
