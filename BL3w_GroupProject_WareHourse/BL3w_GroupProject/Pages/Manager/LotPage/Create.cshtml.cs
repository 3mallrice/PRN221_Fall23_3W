using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Manager.LotPage
{
    public class CreateModel : PageModel
    {
        private readonly ILotService _lotService;

        public CreateModel()
        {
            _lotService = new LotService();
        }

        public IActionResult OnGet()
        {
            ViewData["AccountId"] = new SelectList(_lotService.GetAllLots().Select(x => x.Account).ToList(), "AccountId", "Name");
            ViewData["PartnerId"] = new SelectList(_lotService.GetAllLots().Select(x => x.Partner).ToList(), "PartnerId", "Name");
            return Page();
        }

        [BindProperty]
        public Lot Lot { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Lot.Status = 1;
            _lotService.AddLot(Lot);

            return RedirectToPage("./Index");
        }
    }
}
