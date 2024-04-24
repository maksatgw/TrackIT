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

    }
}
