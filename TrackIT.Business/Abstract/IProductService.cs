using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        List<Product> TGetWithIncludedSearch(string searchQuery);
        List<Product> TGetWithIncluded(int page, int pageSize);
        Product TGetWithIncluded(int id);
        public List<Product> TGetByCategory(int id);
        List<Product> TGetAvailableToRegistrate();
    }
}
