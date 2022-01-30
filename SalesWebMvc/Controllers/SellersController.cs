using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using System;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
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

        public IActionResult Index()
        {
            var list = sellerService.FindAll();
            return View(list);
        }
        // Get Create
        public IActionResult Create()
        {

            var Departments = departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = Departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = departmentService.FindAll();
                var ViewModel = new SellerFormViewModel { Departments = departments };
                return View(ViewModel);
            }
            sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) { return RedirectToAction(nameof(Error), new { message = "Id not Found" }); }

            var obj = sellerService.FindById(id.Value);
            if (obj == null) { return NotFound(); }


            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)

        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "IdNotFound" });
            }
            var obj = sellerService.FindById(id.Value);
            if (obj == null) { return RedirectToAction(nameof(Error), new {message="Id not Found"}); }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            
            if (id == null) { return RedirectToAction(nameof(Error), new { message = "Id not Found" }); }

            var seller = sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new {message=" Id of Seller not Found"});
            }
            List<Department> departments = departmentService.FindAll();

            SellerFormViewModel viewModel = new SellerFormViewModel() { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = departmentService.FindAll();
                var ViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(ViewModel);
            }
            if (id != seller.Id) { 
                return RedirectToAction(nameof(Error), new { message = "Id provided not Equals of Id Seller" });
            }
            try
            {
                sellerService.Update(seller);
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
