using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvC.Models;
using SalesWebMvC.Models.ViewModels;
using SalesWebMvC.Services;
using SalesWebMvC.Services.Exceptions;

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

        //GET: Seller/Index
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
                return RedirectToAction(nameof(Error), new {message = "ID Not Provided" });
            }

            var sellers = _sellerService.FindByID(id.Value);
            
            if (sellers == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Found" });
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
                return RedirectToAction(nameof(Error), new { message = "ID Not Provided" });
            }

            var obj = _sellerService.FindByID(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Found" });
            }

            //_sellerService.Update(obj);
            return View(obj);
        }

        // GET: Seller/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Provided" });
            }

            var sellers = _sellerService.FindByID(id.Value);

            if (sellers == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Found" });
            }

            List<Department> departments = _departmentService.findAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = sellers, Departments = departments };
            return View(viewModel);
        }

        // POST: Seller/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        //Error Mensagem
        public IActionResult Error (string message)
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