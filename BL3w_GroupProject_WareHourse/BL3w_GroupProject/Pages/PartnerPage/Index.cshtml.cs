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
    public class IndexModel : PageModel
    {
        private readonly IPartnerService _context;

        public IndexModel(IPartnerService context)
        {
            _context = context;
        }

        public IList<Partner> Partner { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Partner = _context.GetPartners();
        }
    }
}
