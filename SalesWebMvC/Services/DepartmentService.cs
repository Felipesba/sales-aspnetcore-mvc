using SalesWebMvC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvCContext _context;

        public DepartmentService(SalesWebMvCContext context)
        {
            _context = context;
        }

        public List<Department> findAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

    }
}
