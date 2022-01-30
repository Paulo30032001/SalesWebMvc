using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using System;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService sellerService;
        private readonly DepartmentService departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            this.sellerService = sellerService;
            this.departmentService = departmentService;
        }

        public async Task< IActionResult> Index()
        {
            var list = await sellerService.FindAllAsync();
            return View(list);
        }
        // Get Create
        public async Task<IActionResult> Create()
        {

            var Departments = await departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = Departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await departmentService.FindAllAsync();
                var ViewModel = new SellerFormViewModel { Departments = departments };
                return View(ViewModel);
            }
          await  sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return RedirectToAction(nameof(Error), new { message = "Id not Found" }); }

            var obj =  await sellerService.FindByIdAsync(id.Value);
            if (obj == null) { return NotFound(); }


            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return  RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)

        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "IdNotFound" });
            }
            var obj = await sellerService.FindByIdAsync(id.Value);
            if (obj == null) { return RedirectToAction(nameof(Error), new {message="Id not Found"}); }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null) { return RedirectToAction(nameof(Error), new { message = "Id not Found" }); }

            var seller = await sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new {message=" Id of Seller not Found"});
            }
            List<Department> departments = await departmentService.FindAllAsync();

            SellerFormViewModel viewModel = new SellerFormViewModel() { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments =  await departmentService.FindAllAsync();
                var ViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(ViewModel);
            }
            if (id != seller.Id) { 
                return RedirectToAction(nameof(Error), new { message = "Id provided not Equals of Id Seller" });
            }
            try
            {
               await sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
           catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
           
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };
            return View(viewModel);

        }

    }
}
