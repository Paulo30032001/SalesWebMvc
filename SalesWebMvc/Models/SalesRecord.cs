using SalesWebMvc.Models.Enums;
using System;
namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        public DateTime date { get; set; }

        public double amount { get; set; }
        public SalesStatus status { get; set; }

        public Seller Seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int Id,DateTime date,double amount,SalesStatus status,Seller Seller)
        {
            this.Id = Id;
            this.date = date;
            this.amount = amount;
            this.status = status;
            this.Seller = Seller;
        }


    }
}
