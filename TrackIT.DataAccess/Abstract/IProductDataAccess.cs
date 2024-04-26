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
        List<Product> GetWithIncludedSearch(string searchQuery);
        List<Product> GetWithIncluded();
        Product GetWithIncluded(int id);
        List<Product> GetByCategory(int id);
        List<Product> GetAvailableToRegistrate();
        
    }
}
