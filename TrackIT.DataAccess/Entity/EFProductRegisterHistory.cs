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
            return _appDbContext.RegistirationHistorys
                //include ile product ekliyoruz.
                .Include(x => x.Product)
                //include ile appuser ekliyoruz.
                .Include(x => x.AppUser)
                //listeliyoruz.
                .ToList();
        }
        
        List<ProductRegistirationHistory> IProductRegisterHistoryDataAccess.GetWithIncluded(int productId)
        {
           return _appDbContext.RegistirationHistorys
                //include ile product ekliyoruz.
                .Include(x => x.Product)
                //ThenInclude ile producta category ekliyoruz.
                .ThenInclude(x=>x.Category)
                //Include ile appuser ekliyoruz.
                .Include(x => x.AppUser)
                //Where ile productId si parametreden gelen productId ye eşit olan
                .Where(x=>x.ProductId == productId)
                //RegistrationDate'i yakından uzağa olarak
                .OrderByDescending(x=>x.RegistrationDate)
                //veriyi listeliyoruz.
                .ToList();
        }
    }
}
