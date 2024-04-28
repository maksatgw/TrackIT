using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Abstract
{
    public interface IProductRegisterService : IGenericService<ProductRegistiration>
    {
        int TGetProductRegisteredUserCount(string id);
        List<ProductRegistiration> TGetWithIncluded();
        public List<ProductRegistiration> TGetWithIncludedSearch(string? searchQuery = null, int? categoryId = null, string? userId = null);
        ProductRegistiration TGetByUserId(string userId);
    }
}
