using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Abstract
{
    public interface ILocationDataAccess : IGenericDataAccess<Location>
    {
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde return etmemizi sağlayan metod.
        List<Location> GetWithIncluded();
        //Aynı metodun arama özelliği kazandırılmış hali.
        List<Location> GetWithIncluded(string searchQuery);
    }
}
