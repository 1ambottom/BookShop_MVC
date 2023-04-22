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
    public class CompanyRepository : Repository<Company>,ICompanyRepository
    {
        private AppDbContext _db;
        public CompanyRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Company obj)
        {
            var companyFromDb = _db.Companies.FirstOrDefault(p => p.Id == obj.Id);
            if (companyFromDb != null)
            {
                companyFromDb.Name = obj.Name;
                companyFromDb.StreetAddress  = obj.StreetAddress;
                companyFromDb.City = obj.City;
                companyFromDb.State = obj.State;
                companyFromDb.PostalCode = obj.PostalCode;
                companyFromDb.PhoneNumber = obj.PhoneNumber;
            }
            _db.Update(companyFromDb);
        }
    }
}
