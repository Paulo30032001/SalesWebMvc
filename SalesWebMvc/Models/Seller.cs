using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [DisplayFormat(DataFormatString ="{0:f2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
       public  int DepartmentId { get; set; } 
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>() { };


        public Seller() { }

        public Seller(int Id,string Name,string Email,DateTime BirthDate,double BaseSalary,Department Department)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
            this.BirthDate = BirthDate;
            this.BaseSalary = BaseSalary;
            this.Department = Department;
            
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void removeSales(SalesRecord sr)
        {
            
                Sales.Remove(sr);
            
        }

        public double TotalSales(DateTime initial,DateTime Final)
        {
         double sum = Sales.Where(x => x.date >= initial && x.date <= Final).Select(x => x.amount).DefaultIfEmpty(0.0).Sum();
            return sum;        
        }
    }
}
