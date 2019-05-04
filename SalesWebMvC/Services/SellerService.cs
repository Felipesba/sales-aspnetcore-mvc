﻿using SalesWebMvC.Models;
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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindByID (int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(m => m.Id == id);

        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

    }
}
