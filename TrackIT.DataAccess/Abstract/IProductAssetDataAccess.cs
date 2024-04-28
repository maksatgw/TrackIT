using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Abstract
{
    public interface IProductAssetDataAccess : IGenericDataAccess<ProductAsset>
    {
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde ve productId ye göre return etmemizi sağlayan metod.
        List<ProductAsset> GetWithIncluded(int productId);
        //Aynı metodun tek bir item döndürdüğü hali.
        ProductAsset GetByProductId(int productId);
    }
}
