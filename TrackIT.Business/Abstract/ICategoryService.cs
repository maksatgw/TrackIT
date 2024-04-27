using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<Category> TGetWithIncluded();
        List<Category> TGetWithIncludedSearch(string searchQuery);

    }
}
