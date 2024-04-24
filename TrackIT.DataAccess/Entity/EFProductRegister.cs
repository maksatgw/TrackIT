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
    public class EFProductRegister : GenericRepository<ProductRegistiration>, IProductRegisterDataAccess
    {
        private readonly AppDbContext _appDbContext;
        public EFProductRegister(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int GetProductRegisteredUserCount(string id)
        {
            return _appDbContext.ProductRegistirations.Where(x=>x.AppUserId == id).Count();
        }
    }
}
