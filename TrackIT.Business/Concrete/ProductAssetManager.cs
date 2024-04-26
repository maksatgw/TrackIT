using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Abstract;
using TrackIT.DataAccess.Abstract;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Concrete
{
    public class ProductAssetManager : IProductAssetService
    {
        private readonly IProductAssetDataAccess _service;

        public ProductAssetManager(IProductAssetDataAccess service)
        {
            _service = service;
        }

        public List<ProductAsset> TGet()
        {
            return _service.Get();
        }

        public ProductAsset TGet(int id)
        {
            return _service.Get(id);
        }

        public void TInsert(ProductAsset model)
        {
            _service.Insert(model);
        }

        public void TUpdate(ProductAsset model)
        {
            _service.Update(model);
        }
        public void TDelete(ProductAsset model)
        {
            _service.Delete(model);
        }

        public List<ProductAsset> TGetWithIncluded(int id)
        {
           return _service.GetWithIncluded(id);
        }
    }
}
