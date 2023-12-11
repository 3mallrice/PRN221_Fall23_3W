using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.Manager.LotPage
{
    public class IndexModel : PageModel
    {
        private readonly ILotService _context;

        public IndexModel(ILotService context)
        {
            _context = context;
        }

        public IList<Lot> Lot { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Lot = _context.GetAllLots().ToList();
        }
    }
}
