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

        //Injeção de Dependência
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        //GET: Seller/Index
        public async Task<IActionResult> Index()
        {
            var listSellers = await _sellerService.FindAllAsync();

            return View(listSellers);
        }

        // GET: Seller/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.findAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // POST: Seller/Create/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.findAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // GET: Seller/Delete/id
        public async  Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "ID Not Provided" });
            }

            var sellers = await _sellerService.FindByIDAsync(id.Value);
            
            if (sellers == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Found" });
            }

            return View(sellers);
        }

        // POST: Seller/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //GET: Seller/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Provided" });
            }

            var obj = await _sellerService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Found" });
            }

            //_sellerService.Update(obj);
            return View(obj);
        }

        // GET: Seller/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Provided" });
            }

            var sellers = await _sellerService.FindByIDAsync(id.Value);

            if (sellers == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Not Found" });
            }

            List<Department> departments = await _departmentService.findAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = sellers, Departments = departments };
            return View(viewModel);
        }

        // POST: Seller/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.findAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
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