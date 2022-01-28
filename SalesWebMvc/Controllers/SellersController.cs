using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService sellerService;
        public SellersController(SellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = sellerService.FindAll();
            return View(list);
        }
        // Get Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller service)
        {
            sellerService.Insert(service);
            return RedirectToAction(nameof(Index));
        }


    }
}
