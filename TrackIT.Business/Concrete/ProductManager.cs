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
    public class ProductManager : IProductService
    {
        private readonly IProductDataAccess _service;

        public ProductManager(IProductDataAccess service)
        {
            _service = service;
        }

        public void TDelete(Product model)
        {
            _service.Delete(model);
        }

        public Product TGet(int id)
        {
            return _service.Get(id);

        }

        public List<Product> TGet()
        {
            return _service.Get();
        }

        public void TInsert(Product model)
        {
            _service.Insert(model);
        }

        public void TUpdate(Product model)
        {
            _service.Update(model);
        }
    }
}
