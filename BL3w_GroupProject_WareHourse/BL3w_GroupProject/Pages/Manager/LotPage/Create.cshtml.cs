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
            if (HttpContext.Session.GetString("account") is null || HttpContext.Session.GetString("account") != "manager")
            {
                return RedirectToPage("/Login");
            }

            Lot.Status = 1;
            Lot.AccountId = Account.AccountId;
            Lot.PartnerId = Partner.PartnerId;
            Lot.LotCode = Lot.LotCode.ToUpper();
            _lotService.AddLot(Lot);

            HashSet<int> selectedProductIds = new HashSet<int>(); // Keep track of selected product IDs

            for (int i = 0; i < 5; i++)
            {
                string productIdString = Request.Form[$"Products[{i}].ProductId"];
                string quantityString = Request.Form[$"LotDetails[{i}].Quantity"];

                if (!string.IsNullOrEmpty(productIdString) && int.TryParse(productIdString, out int productId) &&
                    !string.IsNullOrEmpty(quantityString) && int.TryParse(quantityString, out int quantity) && quantity > 0)
                {
                    // Check if the product ID has already been selected
                    if (selectedProductIds.Contains(productId))
                    {
                        ViewData["Error"] = $"Duplicate selection of Product ID: {productId}.";
                        _lotService.DeleteLotPermanently(Lot);
                        var product1 = _productService.GetProductByID(productId);
                        product1.Quantity = 0;
                        _productService.UpdateProduct(product1);
                        InitializeSelectLists();
                        return Page();
                    }

                    selectedProductIds.Add(productId); // Add the product ID to the set

                    var lotDetail = new LotDetail
                    {
                        LotId = Lot.LotId,
                        ProductId = productId,
                        PartnerId = Partner.PartnerId,
                        Status = 1,
                        Quantity = quantity,
                    };

                    LotDetails.Add(lotDetail); // Add to the collection

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