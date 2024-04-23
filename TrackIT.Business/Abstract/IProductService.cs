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
        List<Product> TGetWithIncluded();
        public List<Product> TGetByCategory(int id);
    }
}
