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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDataAccess _service;
        public CategoryManager(ICategoryDataAccess service)
        {
            _service = service;
        }
        public List<Category> TGet()
        {
            return _service.Get();
        }

        public Category TGet(int id)
        {
            return _service.Get(id);
        }

        public void TInsert(Category model)
        {
            _service.Insert(model);
        }

        public void TUpdate(Category model)
        {
            _service.Update(model);
        }
        public void TDelete(Category model)
        {
            _service.Delete(model);
        }
    }
}
