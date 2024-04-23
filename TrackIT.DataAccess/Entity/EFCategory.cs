using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.DataAccess.Abstract;
using TrackIT.DataAccess.Concrete;
using TrackIT.DataAccess.Repository;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Entity
{
    public class EFCategory : GenericRepository<Category>, ICategoryDataAccess
    {
        public EFCategory(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
