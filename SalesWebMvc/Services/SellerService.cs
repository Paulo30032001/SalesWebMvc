using SalesWebMvc.Models;
using SalesWebMvc.Data;
using System.Collections.Generic;
using System.Linq;
namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
        
        public SellerService(SalesWebMvcContext _context)
        {
            this._context = _context;
            
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
           
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(x=>x.Id==id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Remove(obj);
            _context.SaveChanges();
        }
    }
}
