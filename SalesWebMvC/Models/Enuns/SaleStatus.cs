using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvC.Models.Enuns
{
    public enum SaleStatus : int
    {
       Pending = 1,
       Billed = 2,
       Cancelated = 3,
    }
}
