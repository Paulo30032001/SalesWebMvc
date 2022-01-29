using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using System;
using SalesWebMvc.Services.Exceptions;
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
            sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) { return NotFound(); }

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

        public IActionResult Details(int id)
        {
            var obj = sellerService.FindById(id);
            if (obj == null) { return NotFound(); }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var seller = sellerService.FindById(id.Value);

            if (seller == null)
            {
                return NotFound();
            }
            List<Department> departments = departmentService.FindAll();

            SellerFormViewModel viewModel = new SellerFormViewModel() { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id) { return BadRequest(); }
            try
            {
                sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundExcpetion)
            {
                return NotFound();
            }
            catch(DbConcurrencyException){

                return NotFound();

            }
        }
    }
}
