using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrackIT.DataAccess.Abstract;
using TrackIT.DataAccess.Concrete;
using TrackIT.DataAccess.Repository;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Entity
{
    public class EFProduct : GenericRepository<Product>, IProductDataAccess
    {
        private readonly AppDbContext _appDbContext;

        public EFProduct(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Product> GetWithIncludedSearch(string searchQuery)
        {
            return _appDbContext.Product
                //Include ile category ekliyoruz.
                .Include(x => x.Category)
                .Include(x => x.Location)
                //where ile name'i searchquery içeren
                //veya serial'ı searchquery içeren
                //veya categorynin name'i searchQuery içeren
                //veya dateaddedi searchQuery içeren veriyi
                .Where(x => x.Name
                .Contains(searchQuery)
                || x.Serial.Contains(searchQuery)
                || x.Description.Contains(searchQuery)
                || x.Category.Name.Contains(searchQuery)
                || x.Location.Name.Contains(searchQuery)
                || x.DateAdded.ToString().Contains(searchQuery)
                )
                //listeliyoruz.
                .ToList();

        }

        public List<Product> GetWithIncluded(int page = 1, int pageSize = 10)
        {
            return _appDbContext.Product
                //Include ile category ekliyoruz
                .Include(x => x.Category)
                .Include(x => x.Location)
                //Include ile productRegister ekliyoruz
                .Include(x => x.ProductRegistirationHistory)
                //gelen veriye göre hangi kayıtların atlanacağını atlar.
                //örneğin. 3. sayfa ve her sayfa 10 kayıt ise
                //(3-1) * 10 = 20. ilk 20 kaydı atlar.
                .Skip((page - 1) * pageSize)
                //pageSize adetinde veri almayı sağlar.
                .Take(pageSize)
                .ToList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _appDbContext.Product
                .Include(x => x.Category)
                .Include(x => x.Location)
                //Where ile categoryId si parametreden gelen categoryIdye eşit olanları
                .Where(x => x.CategoryId.Equals(categoryId))
                //listeliyoruz.
                .ToList();
        }

        public List<Product> GetAvailableToRegistrate()
        {
            return _appDbContext.Product
                //Where ile ProductRegistration içinde olan herhangi bir verinin listelenmemesini sağlıyoruz.
                .Where(x => !_appDbContext.ProductRegistirations.Any(pr => pr.ProductId == x.ProductId))
                .ToList();
        }

        public Product GetWithIncluded(int id)
        {
            return _appDbContext.Product
                //Include ile category ekliyoruz
                .Include(x => x.Category)
                .Include(x => x.Location)
                //ProductRegistrationHistory ekliyoruz
                .Include(x => x.ProductRegistirationHistory)
                //FirstOrDefautlt ile productId si parametreden gelen id ye eşit olan ilk veriyi getiriyoruz.
                .FirstOrDefault(x => x.ProductId == id);
        }

        public List<Product> GetWithIncluded()
        {
            return _appDbContext.Product
                .Include(x => x.Category)
                .Include(x => x.Location)
                .ToList();
        }

        public List<Product> GetByLocation(int locationId)
        {
            return _appDbContext.Product
                .Include(x => x.Category)
                .Include(x => x.Location)
                //Where ile locationId si parametreden gelen categoryIdye eşit olanları
                .Where(x => x.LocationId.Equals(locationId))
                //listeliyoruz.
                .ToList();
        }
    }

}