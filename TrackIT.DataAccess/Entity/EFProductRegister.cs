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
    public class EFProductRegister : GenericRepository<ProductRegistiration>, IProductRegisterDataAccess
    {
        private readonly AppDbContext _appDbContext;
        public EFProductRegister(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public int GetProductRegisteredUserCount(string id)
        {
            //AppUserId ile parametreden gelen değeri eşit olan verilerin sayısını (count) alıyoruz.
            return _appDbContext.ProductRegistirations.Where(x => x.AppUserId == id).Count();
        }

        public List<ProductRegistiration> GetWithIncluded()
        {
            return _appDbContext.ProductRegistirations
                //Include ile product'ı ekliyoruz.
                .Include(x => x.Product)
                //ThenInclude ile eklediğimiz product'a category ekliyoruz.
                .ThenInclude(p => p.Category)
                //Include ile appUser ekliyoruz.
                .Include(x => x.AppUser)
                //Where kullanarak appuser active değeri true olan değerleri getiriyoruz.
                //böylelikle kullanıcıları sistemden db üzerinden silmeden sistemden çıkarabiliyoruz.
                //daha sonra değineceğimiz bu sistem ile productRegisterHistory tarafında user dururken registerde durmayacak
                .Where(x => x.AppUser.isActive == true)
                .ToList();
        }

        public List<ProductRegistiration> GetWithIncludedSearch(string? searchQuery = null, int? categoryId = null, string? userId = null)
        {
            //categoryId null değilse
            if (categoryId != null)
            {
                return _appDbContext.ProductRegistirations
                    //Include ile product ekliyoruz.
                    .Include(x => x.Product)
                    //ThenInclude ile product'a category ekliyoruz.
                    .ThenInclude(p => p.Category)
                    //Include ile appuser ekliyoruz.
                    .Include(x => x.AppUser)
                    //Where ile product idsi parametreden gelen category id
                    // ve appUser isactive true olan değerleri listeliyoruz.
                    .Where(x => x.Product.CategoryId == categoryId && x.AppUser.isActive == true)
                    .ToList();
            }
            //searchquery null veya boş değilse
            else if (!string.IsNullOrEmpty(searchQuery))
            {
                return _appDbContext.ProductRegistirations
                    //Include ile product ekliyoruz.
                    .Include(x => x.Product)
                    //ThenInclude ile product'a category ekliyoruz.
                    .ThenInclude(p => p.Category)
                    //Include ile appuser ekliyoruz.
                    .Include(x => x.AppUser)
                    //Where ile product name'i
                    //veya product category name'i
                    //veya product username'i
                    //veya product description'u 
                    //veya product serial'ı 
                    //ve appUser isactive true olan değerleri listeliyoruz.
                    .Where(x => x.Product.Name.Contains(searchQuery)
                    || x.Product.Category.Name.Contains(searchQuery)
                    || x.AppUser.UserName.Contains(searchQuery)
                    || x.Product.Description.Contains(searchQuery)
                    || x.Product.Serial.Contains(searchQuery)
                    && x.AppUser.isActive == true
                    )
                    .ToList();
            }
            //userId null veya boş değilse
            else if (!string.IsNullOrEmpty(userId))
            {
                return _appDbContext.ProductRegistirations
                    //Include ile product ekliyoruz
                    .Include(x => x.Product)
                    //thenInclude ile product'a category ekliyoruz.
                    .ThenInclude(p => p.Category)
                    //Include ile appUser ekliyoruz.
                    .Include(x => x.AppUser)
                    //Where ile appUserId'si parametreden gelen userId ye eşit olan
                    //ve AppUser isactive true olan değerleri listeliyoruz.
                    .Where(x => x.AppUserId == userId && x.AppUser.isActive == true)
                    .ToList();
            }
            //hiçbir senaryo değilse GetWithInclude metodunu çağır.
            return GetWithIncluded();
        }

        public ProductRegistiration GetByUserId(string userId)
        {
            //appuserId si parametreden gelen userIdye eşit olan ilk değeri getir diyoruz.
            return _appDbContext.ProductRegistirations
                .FirstOrDefault(x => x.AppUserId == userId);
        }
    }
}
