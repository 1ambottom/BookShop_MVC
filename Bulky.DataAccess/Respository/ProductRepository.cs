using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private AppDbContext _db;
        public ProductRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var productFromDb = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (productFromDb != null)
            {
                productFromDb.Title = obj.Title;
                productFromDb.Description = obj.Description;
                productFromDb.ISBN = obj.ISBN;
                productFromDb.Author = obj.Author;
                productFromDb.ListPrice = obj.ListPrice;
                productFromDb.Price = obj.Price;
                productFromDb.Price50 = obj.Price50;
                productFromDb.Price100 = obj.Price100;
                productFromDb.CategoryId = obj.CategoryId;
                productFromDb.Price100 = obj.Price100;
                if (productFromDb.ImageUrl != null)
                {
                    productFromDb.ImageUrl = obj.ImageUrl;
                }
            }
            _db.Update(productFromDb);
        }
    }
}
