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
    public class EFCategory : GenericRepository<Category>, ICategoryDataAccess
    {
        private readonly AppDbContext _appDbContext;
        public EFCategory(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Category> GetWithIncluded()
        {
            return _appDbContext.Categories.Include(x => x.Products).ToList();
        }

        public List<Category> GetWithIncludedSearch(string searchQuery)
        {
            return _appDbContext.Categories.Include(x => x.Products)
                .Where(x => x.Name.Contains(searchQuery))
                .ToList();
        }
    }
}
