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
    public class EFProductAsset : GenericRepository<ProductAsset>, IProductAssetDataAccess
    {
        private readonly AppDbContext _appDbContext;
        public EFProductAsset(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<ProductAsset> GetWithIncluded(int id)
        {
           return _appDbContext.ProductAssets.Include(x => x.Product).Where(x => x.ProductId == id).ToList();
        }
    }
}
