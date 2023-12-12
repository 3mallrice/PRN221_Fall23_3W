using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;
using Microsoft.Identity.Client.Extensions.Msal;

namespace BL3w_GroupProject.Pages.Manager.LotPage
{
    public class CreateModel : PageModel
    {
        private readonly ILotService _lotService;
        private readonly IAccountService _accService;
        private readonly IProductService _productService;
        private readonly IPartnerService _partnerService;
        public CreateModel()
        {
            _lotService = new LotService();
            _productService = new ProductService();
            _partnerService = new PartnerService();
            _accService = new AccountService();
        }

        public IActionResult OnGet()
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

            ViewData["AccountId"] = new SelectList(_accService.GetAccounts().Where(x => x.Status != 0).ToList(), "AccountId", "Email");
            ViewData["PartnerId"] = new SelectList(_partnerService.GetPartners().Where(x => x.Status != 0), "PartnerId", "Name");
            ViewData["ProductId"] = new SelectList(_productService.GetProducts().Where(x => x.Quantity == 0), "ProductId", "Name");
            return Page();
        }

        [BindProperty]
        public Lot Lot { get; set; } = default!;
        [BindProperty]
        public LotDetail LotDetail { get; set; } = default!;
        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public Partner Partner { get; set; } = default!;
        [BindProperty]
        public Account Account { get; set; } = default!;
        [BindProperty]
        public List<LotDetail> LotDetails { get; set; } = new List<LotDetail>();

        [BindProperty]
        public List<Product> Products { get; set; } = new List<Product>();
        public async Task<IActionResult> OnPostAsync()
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

            var existingProduct = _lotService.GetAllLots()
                .SelectMany(lot => lot.LotDetails)
                .Any(detail => detail.Product != null &&
                       (detail.Product.ProductId == Product.ProductId || detail.Product.Name == Product.Name) &&
                       detail.Quantity > 0);

            if (existingProduct)
            {
                ViewData["Error"] = "Product name or ID already selected with a quantity greater than 0.";
                InitializeSelectLists();
                return Page();
            }

            /*            if (LotDetail.Quantity <= 0)
                        {
                            ViewData["Error"] = "Quantity must be greater than 0.";
                            InitializeSelectLists();
                            return Page();
                        }

                        if (LotDetail.Quantity > 0)
                        {
                            Lot.Status = 1;
                            Lot.AccountId = Account.AccountId;
                            Lot.PartnerId = Partner.PartnerId;
                            Lot.LotCode = Lot.LotCode.ToUpper();
                            _lotService.AddLot(Lot);

                            if (Product.ProductId != 0)
                            {
                                LotDetail.LotId = Lot.LotId;
                                LotDetail.ProductId = Product.ProductId;
                                LotDetail.PartnerId = Partner.PartnerId;
                                LotDetail.Status = 1;
                                _lotService.AddLotDetail(LotDetail);

                                var product = _productService.GetProductByID(Product.ProductId);
                                product.Quantity = LotDetail.Quantity;
                                _productService.UpdateProduct(product);
                            }
                            return RedirectToPage("./Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Quantity must be greater than 0.";
                            InitializeSelectLists();
                            return Page();
                        }*/
            Lot.Status = 1;
            Lot.AccountId = Account.AccountId;
            Lot.PartnerId = Partner.PartnerId;
            Lot.LotCode = Lot.LotCode.ToUpper();
            _lotService.AddLot(Lot);
            for (int i = 0; i < 5; i++)
            {
/*                int productId = Convert.ToInt32(Request.Form[$"Products[{i}].ProductId"]);
                int quantity = Convert.ToInt32(Request.Form[$"LotDetails[{i}].Quantity"]);*/

                string productIdString = Request.Form[$"Products[{i}].ProductId"];
                string quantityString = Request.Form[$"LotDetails[{i}].Quantity"];
                // Check if quantity is greater than 0
                if (!string.IsNullOrEmpty(productIdString) && int.TryParse(productIdString, out int productId) &&
                    !string.IsNullOrEmpty(quantityString) && int.TryParse(quantityString, out int quantity) && quantity > 0)
                {
                    // Your logic here...
                    var lotDetail = new LotDetail
                    {
                        LotId = Lot.LotId,
                        ProductId = productId,
                        PartnerId = Partner.PartnerId,
                        Status = 1,
                        Quantity = quantity,
                    };

                    // Add LotDetail entry to the database or your data store
                    _lotService.AddLotDetail(lotDetail);

                    // Update Product quantity if needed
                    var product = _productService.GetProductByID(productId);
                    product.Quantity = quantity;
                    _productService.UpdateProduct(product);
                }
            }
            return RedirectToPage("./Index");
        }
        private void InitializeSelectLists()
        {
            ViewData["AccountId"] = new SelectList(_lotService.GetAllLots().Select(x => x.Account).ToList(), "AccountId", "Email");
            ViewData["PartnerId"] = new SelectList(_lotService.GetAllLots().Select(x => x.Partner).ToList(), "PartnerId", "Name");
            ViewData["ProductId"] = new SelectList(_productService.GetProducts().Where(x => x.Quantity == 0), "ProductId", "Name");
        }
    }
}