using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TDelete(T model);
        void TInsert(T model);
        void TUpdate(T model);
        T TGet(int id);
        List<T> TGet();
    }
}
