using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.DataAccess.Abstract;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Abstract
{
    public interface IProductRegisterHistoryService : IGenericService<ProductRegistirationHistory>
    {
        List<ProductRegistirationHistory> TGetWithIncluded();
        List<ProductRegistirationHistory> TGetWithIncluded(int id);
    }
}
