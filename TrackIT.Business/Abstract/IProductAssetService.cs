using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Abstract
{
    public interface IProductAssetService : IGenericService<ProductAsset>
    {
        List<ProductAsset> TGetWithIncluded(int id);
        ProductAsset TGetByProductId(int id);
    }
}
