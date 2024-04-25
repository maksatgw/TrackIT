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
        int GetProductRegisteredUserCount(string id);
        List<ProductRegistiration> GetWithIncluded();
        List<ProductRegistiration> GetWithIncludedSearch(string? searchQuery = null, int? categoryId = null, string? userId = null);
        
    }
}
