using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} - campo requerido")]
        [StringLength(60,MinimumLength =3, ErrorMessage ="{0} tamanho deve ser entre {2} e {1} caracteres")]
        public String Name { get; set; }

        [Required(ErrorMessage = "{0} - campo requerido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Entre com um {0} válido")]
        public String email { get; set; }

        [Required(ErrorMessage = "{0} - campo requerido")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "{0} - campo requerido")]
        [Range(1000,50000, ErrorMessage ="O {0} deve ser de {1} a {2}")]
        [Display(Name = "Base Salary")]
        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthdate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            this.email = email;
            Birthdate = birthdate;
            BaseSalary = baseSalary;
            Department = department;
        }
          
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
                         
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);

        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }






    }
}
