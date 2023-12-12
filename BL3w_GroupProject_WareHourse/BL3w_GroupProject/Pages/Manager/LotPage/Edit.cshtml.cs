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

namespace BL3w_GroupProject.Pages.Manager.LotPage
{
    public class EditModel : PageModel
    {
        private readonly ILotService lotService;
        private readonly IAccountService accountService;
        private readonly IPartnerService partnerService;
        public EditModel()
        {
            lotService = new LotService();
            accountService = new AccountService();
            partnerService = new PartnerService();
        }

        [BindProperty]
        public Lot Lot { get; set; } = default!;       
        [BindProperty]
        public LotDetail LotDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "manager")
            {
                return RedirectToPage("/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var lot =  lotService.GetLotById((int)id);
            if (lot == null)
            {
                return NotFound();
            }
            Lot = lot;
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "AccountId", "Email");
            ViewData["PartnerId"] = new SelectList(partnerService.GetPartners(), "PartnerId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                lotService.UpdateLot(Lot);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(Lot.LotId))
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

        private bool LotExists(int id)
        {
          return lotService.GetLotByAccountId((int)id) != null;
        }
    }
}
