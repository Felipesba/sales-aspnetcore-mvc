using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvC.Services;

namespace SalesWebMvC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

   
        public IActionResult Index()
        {
            var listSellers = _sellerService.findAll();

            return View(listSellers);
        }
    }
}