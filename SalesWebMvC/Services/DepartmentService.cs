using SalesWebMvC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvCContext _context;

        public DepartmentService(SalesWebMvCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> findAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
