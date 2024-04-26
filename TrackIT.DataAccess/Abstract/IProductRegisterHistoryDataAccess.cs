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
        List<ProductRegistirationHistory> GetWithIncluded();
        List<ProductRegistirationHistory> GetWithIncluded(int id);

    }
}
