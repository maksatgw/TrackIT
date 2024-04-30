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
    public class LocationManager : ILocationService
    {
        private readonly ILocationDataAccess _service;

        public LocationManager(ILocationDataAccess service)
        {
            _service = service;
        }

        public void TDelete(Location model)
        {
            _service.Delete(model);
        }

        public Location TGet(int id)
        {
            return _service.Get(id);
        }

        public List<Location> TGet()
        {
            return _service.Get();
        }

        public List<Location> TGetWithIncluded()
        {
            return _service.GetWithIncluded();
        }

        public List<Location> TGetWithIncluded(string searchQuery)
        {
            return _service.GetWithIncluded(searchQuery);
        }

        public void TInsert(Location model)
        {
            _service.Insert(model);
        }

        public void TUpdate(Location model)
        {
            _service.Update(model);
        }
    }
}
