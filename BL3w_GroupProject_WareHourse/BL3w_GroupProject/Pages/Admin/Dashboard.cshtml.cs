using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace BL3w_GroupProject.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ILotService _lotService;
        private readonly IStockOutService _stockOutService;
        private readonly ICategoryService _categoryService;
        private readonly IPartnerService _partnerService;
        private readonly IProductService _productService;

        public DashboardModel(IProductService productService, 
            IPartnerService partnerService, 
            ILotService lotService, 
            IStockOutService stockOutService, 
            ICategoryService categoryService, 
            IAccountService accountService)
        {
            _productService = productService;
            _lotService = lotService;
            _stockOutService = stockOutService;
            _categoryService = categoryService;
            _accountService = accountService;
            _partnerService = partnerService;
        }

        public int ProductCount { get; private set; }
        public int LotCount { get; private set; }
        public int AccountCount { get; private set; }
        public int PartnerCount { get; private set; }
        public int CategoryCount { get; private set; }
        public int StockOutCount { get; private set; }
        public int StockOut1 {  get; private set; }
        public int StockOut0 { get; private set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "admin")
            {
                return RedirectToPage("/Login");
            }

            ProductCount = _productService.GetProducts().Count();
            LotCount = _lotService.GetAllLots().Count();
            AccountCount = _accountService.GetAccounts().Count();
            PartnerCount = _partnerService.GetPartners().Count();
            CategoryCount = _categoryService.GetCategories().Count();
            StockOutCount = _stockOutService.GetStockOuts().Count();

            StockOut1 = _stockOutService.GetStockOuts().Where(s => s.Status == 1).ToList().Count();
            StockOut0 = _stockOutService.GetStockOuts().Where(s => s.Status == 0).ToList().Count();

            return Page();
        }
    }
}
