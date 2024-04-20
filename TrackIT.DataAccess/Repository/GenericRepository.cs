using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.DataAccess.Abstract;
using TrackIT.DataAccess.Concrete;

namespace TrackIT.DataAccess.Repository
{
    //Bu metodlar haricinde sınıfların ihtiyaçları için yeni Repositoryler açılması gerekmektedir.
    //Sınıfa özgü Repositoryler bu Repository'den kalıtım almalıdır.
    public class GenericRepository<T> : IGenericDataAccess<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<T> Get()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public void Insert(T item)
        {
            _appDbContext.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Update(T item)
        {
            _appDbContext.Update(item);
            _appDbContext.SaveChanges();
        }
        public void Delete(T item)
        {
            _appDbContext.Remove(item);
            _appDbContext.SaveChanges();
        }
    }
}
