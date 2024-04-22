using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _appDbContext.Product.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(searchQuery.ToLower())).ToList();

        }

        public List<Product> GetWithIncluded()
        {
            return _appDbContext.Product.Include(x => x.Category).ToList();
          
        }

       
    }

}