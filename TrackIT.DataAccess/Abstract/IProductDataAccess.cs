using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Abstract
{
    public interface IProductDataAccess : IGenericDataAccess<Product>
    {
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde
        //ve aramaya göre return etmemizi sağlayan metod.
        List<Product> GetWithIncludedSearch(string searchQuery);
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde
        //ve filtrelere göre return etmemizi sağlayan metod.
        List<Product> GetWithIncluded(int page, int pageSize);
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde
        //tek bir item döndüren metod
        Product GetWithIncluded(int id);
        //Tabloyu ilişkili olduğu categoryId sine göre getiren metod
        List<Product> GetByCategory(int categoryId);
        //Tabloyu register'e uygunluğuna göre getiren metod.
        List<Product> GetAvailableToRegistrate();

    }
}
