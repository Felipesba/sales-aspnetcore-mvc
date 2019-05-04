using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvC.Models;
using SalesWebMvC.Models.ViewModels;
using SalesWebMvC.Services;

namespace SalesWebMvC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }


        public IActionResult Index()
        {
            var listSellers = _sellerService.FindAll();

            return View(listSellers);
        }

        // GET: Seller/Create
        public IActionResult Create()
        {
            var departments = _departmentService.findAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // POST: Seller/Create/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        // GET: Seller/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellers = _sellerService.FindByID(id.Value);
            
            if (sellers == null)
            {
                return NotFound();
            }

            return View(sellers);
        }

        // POST: Seller/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //GET: Seller/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }

    }