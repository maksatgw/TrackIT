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
    public class EFLocation : GenericRepository<Location>, ILocationDataAccess
    {
        private readonly AppDbContext _appDbContext;
        public EFLocation(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Location> GetWithIncluded()
        {
           return _appDbContext.Locations
                .Include(x=>x.Products)
                .ToList();
        }

        public List<Location> GetWithIncluded(string searchQuery)
        {
            return _appDbContext.Locations
                .Include(x => x.Products)
                .Where(x=>x.Name.Contains(searchQuery) || x.Description.Contains(searchQuery))
                .ToList();
        }
    }
}
