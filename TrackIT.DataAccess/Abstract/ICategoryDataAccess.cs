using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Abstract
{
    public interface ICategoryDataAccess : IGenericDataAccess<Category>
    {
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde return etmemizi sağlayan metod.
        List<Category> GetWithIncluded();
        //Aynı metodun arama özelliği kazandırılmış hali.
        List<Category> GetWithIncludedSearch(string searchQuery);
    }
}
