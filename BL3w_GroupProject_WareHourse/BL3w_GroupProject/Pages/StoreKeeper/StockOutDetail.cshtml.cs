using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;

namespace BL3w_GroupProject.Pages.StoreKeeper
{
    public class StockOutDetailModel : PageModel
    {
        private readonly IStockOutService _stockOutService;

        public StockOutDetailModel(IStockOutService stockOutService)
        {
            _stockOutService = stockOutService;
        }

        public StockOut StockOut { get; set; } = default!;

        public List<StockOutDetail> StockOutDetails { get; set; } = new List<StockOutDetail>();

        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            StockOut = _stockOutService.GetStockOutById(id);
            if (StockOut == null)
            {
                return NotFound();
            }
            else
            {
                var stockOutDetail = _stockOutService.GetStockOutsDetail().Where(s => s.StockOutDetailId == id);
            }
            return Page();
        }
    }
}
