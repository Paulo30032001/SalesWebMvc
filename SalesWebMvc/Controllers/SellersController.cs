using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
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
    }
}
