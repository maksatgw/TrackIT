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
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde getiren metod.
        public List<Category> GetWithIncluded()
        {
            //bunu sağlayan include metodu
            return _appDbContext.Categories.Include(x => x.Products).ToList();
        }
        //Aynı metodun arama özelliği kazandırılmış hali.
        public List<Category> GetWithIncludedSearch(string searchQuery)
        {
            //include ile products tablosunu ekliyoruz.
            //where ile category adında searchquery içeren dataları buluyoruz.
            return _appDbContext.Categories
                .Include(x => x.Products)
                .Where(x => x.Name.Contains(searchQuery))
                .ToList();
        }
    }
}
