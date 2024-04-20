using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DataAccess.Abstract
{
    public interface IGenericDataAccess<T> where T : class
    {
        //Bu metodlar haricinde sınıfların ihtiyaçları için yeni interfaceler açılması gerekmektedir.
        //Sınıfa özgü interfaceler bu interface'den kalıtım almalıdır.
        List<T> Get();
        T Get(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);

    }
}
