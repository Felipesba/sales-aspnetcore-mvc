using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvC.Models
{
    public class SalesWebMvCContext : DbContext
    {
        public SalesWebMvCContext (DbContextOptions<SalesWebMvCContext> options)
            : base(options)
        {
        }

        public DbSet<SalesWebMvC.Models.Department> Department { get; set; }
    }
}
