using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Abstract
{
    public interface IProductRegisterDataAccess : IGenericDataAccess<ProductRegistiration>
    {
        //Kullanıcılara register edilmiş productların sayısını
        //user idsine göre getiren metod.
        int GetProductRegisteredUserCount(string id);
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde getiren metod.
        List<ProductRegistiration> GetWithIncluded();
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde
        //arama ve filtrelere göre getiren metod.
        //parametreler optional olduğu için default değer null olarak atanmış.
        List<ProductRegistiration> GetWithIncludedSearch(string? searchQuery = null, int? categoryId = null, string? userId = null);
        //Registerları userId'ye göre getiren metod.
        ProductRegistiration GetByUserId(string userId);
    }
}
