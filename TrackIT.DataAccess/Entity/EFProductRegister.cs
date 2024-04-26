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
            return _appDbContext.ProductRegistirations.Where(x => x.AppUserId == id).Count();
        }

        public List<ProductRegistiration> GetWithIncluded()
        {
            return _appDbContext.ProductRegistirations.Include(x => x.Product).ThenInclude(p => p.Category).Include(x => x.AppUser).ToList();
        }

        public List<ProductRegistiration> GetWithIncludedSearch(string? searchQuery = null, int? categoryId = null, string? userId = null)
        {
            if (categoryId != null)
            {
                return _appDbContext.ProductRegistirations
                    .Include(x => x.Product)
                    .ThenInclude(p => p.Category)
                    .Include(x => x.AppUser)
                    .Where(x => x.Product.CategoryId == categoryId)
                    .ToList();
            }
            else if (!string.IsNullOrEmpty(searchQuery))
            {
                return _appDbContext.ProductRegistirations.Include(x => x.Product)
                    .ThenInclude(p => p.Category)
                    .Include(x => x.AppUser)
                    .Where(x => x.Product.Name.Contains(searchQuery) 
                    || x.Product.Category.Name.Contains(searchQuery) 
                    || x.AppUser.UserName.Contains(searchQuery)
                    || x.Product.Description.Contains(searchQuery)
                    || x.Product.Serial.Contains(searchQuery)
                    )
                    .ToList();
            }
            else if (!string.IsNullOrEmpty(userId))
            {
                return _appDbContext.ProductRegistirations.Include(x => x.Product)
                    .ThenInclude(p => p.Category)
                    .Include(x => x.AppUser)
                    .Where(x => x.AppUserId == userId)
                    .ToList();
            }
            return GetWithIncluded();
        }
    }
}
