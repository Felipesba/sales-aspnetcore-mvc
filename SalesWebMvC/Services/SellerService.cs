using SalesWebMvC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvC.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvC.Services
{
    public class SellerService
    {
        private readonly SalesWebMvCContext _context;

        public SellerService(SalesWebMvCContext context)
        {
            _context = context;
        }

        public List<Seller> findAll() {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    
    }
}
