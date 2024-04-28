using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Abstract
{
    public interface IProductRegisterHistoryDataAccess : IGenericDataAccess<ProductRegistirationHistory>
    {
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde getiren metod.
        List<ProductRegistirationHistory> GetWithIncluded();
        //Tabloyu ilişkilere tablo üzerinden ulaşabilecek şekilde ve product idsine göre getiren metod.
        List<ProductRegistirationHistory> GetWithIncluded(int productId);

    }
}
