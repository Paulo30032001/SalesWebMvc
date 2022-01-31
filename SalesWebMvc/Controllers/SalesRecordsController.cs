using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService salesService;

        public SalesRecordsController(SalesRecordService salesService)
        {
            this.salesService = salesService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task< IActionResult> SimpleSearch( DateTime? minDate,DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await salesService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
