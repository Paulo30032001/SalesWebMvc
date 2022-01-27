using System;
using System.Collections.Generic;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>() { } ;

        public Department() { }

        public Department(int Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public void AddSeler(Seller Seller)
        {
            Sellers.Add(Seller);
        }

        public double TotalSales(DateTime initial,DateTime final)
        {
            return Sellers.Sum(x => x.TotalSales(initial, final));  
         }

    }
}
