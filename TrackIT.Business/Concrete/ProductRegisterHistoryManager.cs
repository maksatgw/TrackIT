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
    public class ProductRegisterHistoryManager : IProductRegisterHistoryService
    {
        private readonly IProductRegisterHistoryDataAccess _service;

        public ProductRegisterHistoryManager(IProductRegisterHistoryDataAccess service)
        {
            _service = service;
        }

        public void TDelete(ProductRegistirationHistory item)
        {
            _service.Delete(item);
        }

        public List<ProductRegistirationHistory> TGet()
        {
            return _service.Get();
        }

        public ProductRegistirationHistory TGet(int id)
        {
            return _service.Get(id);
        }

        public List<ProductRegistirationHistory> TGetWithIncluded()
        {
            return _service.GetWithIncluded();
        }

        public List<ProductRegistirationHistory> TGetWithIncluded(int id)
        {
            return _service.GetWithIncluded(id);
        }

        public void TInsert(ProductRegistirationHistory item)
        {
            _service.Insert(item);
        }

        public void TUpdate(ProductRegistirationHistory item)
        {
            _service.Update(item);
        }
    }
}
