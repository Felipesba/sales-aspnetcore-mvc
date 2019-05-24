using SalesWebMvC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvC.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvC.Services.Exceptions;


namespace SalesWebMvC.Services
{
    public class SellerService
    {
        private readonly SalesWebMvCContext _context;

        public SellerService(SalesWebMvCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIDAsync (int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            if (!await _context.Seller.AnyAsync(x => x.Id == obj.Id))
            { 
                throw new NotFoundException("ID não encontrado");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
           
        }

    }
}
