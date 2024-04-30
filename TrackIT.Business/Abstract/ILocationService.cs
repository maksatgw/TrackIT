using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.DataAccess.Abstract;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Abstract
{
    public interface ILocationService : IGenericService<Location>
    {
        List<Location> TGetWithIncluded();
        List<Location> TGetWithIncluded(string searchQuery);

    }
}
