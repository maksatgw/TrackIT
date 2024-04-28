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

        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde ve productId ye göre return etmemizi sağlayan metod.
        public List<ProductAsset> GetWithIncluded(int productId)
        {
            //include ile productları ekliyoruz.
            //where ile product idsi parametreden gelen product id ye eşit olan dataları listeliyor.
            return _appDbContext.ProductAssets.Include(x => x.Product)
                 .Where(x => x.ProductId == productId).ToList();
        }
        //Aynı metodun tek bir item döndürdüğü hali.
        public ProductAsset GetByProductId(int productId)
        {
            //include ile productları ekliyoruz.
            //firstordefault ile productIdsi parametreden gelen değerle aynı olan ilk değeri getirecek
            //eğer veri gelmez ise null döndürecek.
            return _appDbContext.ProductAssets.Include(x => x.ProductId)
                .FirstOrDefault(x => x.ProductId == productId);
        }
    }
}
