﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService sellerService;
        private readonly DepartmentService departmentService;
        public SellersController(SellerService sellerService,DepartmentService departmentService)
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


    }
}
