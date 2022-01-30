using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage =" {0} Preenchimento Obrigatorio")]
        [StringLength(60,MinimumLength =2,ErrorMessage ="O {0} deve estar entre {2} e {1}")]
        public string Name { get; set; }
      

        
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Enter a valid Email")]
        [Required(ErrorMessage ="{0} Preenchimento Obrigatorio")]
        public string Email { get; set; }

       [Required(ErrorMessage ="{0}Preenchimento Obrigatorio")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "{0} Preenchimento Obrigatorio")]
        [Range(100,50000.0,ErrorMessage ="{0} deve ser entre {1} e {2}")]
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
