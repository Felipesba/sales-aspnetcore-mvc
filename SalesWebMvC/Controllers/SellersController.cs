using System;
using System.Collections.Generic;
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

            _sellerService.Update(obj);
            return RedirectToAction(nameof(Index));
        }

        // GET: Seller/Edit/id
        public IActionResult Edit(int? id)
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
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException e)
            {

                throw new NotFoundException(e.Message);
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }



    }

    }