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
    public class EFProductRegisterHistory : GenericRepository<ProductRegistirationHistory>, IProductRegisterHistoryDataAccess
    {
        private readonly AppDbContext _appDbContext;
        public EFProductRegisterHistory(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<ProductRegistirationHistory> GetWithIncluded()
        {
            return _appDbContext.RegistirationHistorys.Include(x => x.Product).Include(x => x.AppUser).ToList();
        }
        
        List<ProductRegistirationHistory> IProductRegisterHistoryDataAccess.GetWithIncluded(int id)
        {
           return _appDbContext.RegistirationHistorys
                .Include(x => x.Product)
                .ThenInclude(x=>x.Category)
                .Include(x => x.AppUser)
                .Where(x=>x.ProductId == id)
                .OrderByDescending(x=>x.RegistrationDate)
                .ToList();
        }
    }
}
