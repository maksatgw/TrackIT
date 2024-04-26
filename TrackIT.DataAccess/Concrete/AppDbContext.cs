using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;
using TrackIT.Entity.Model;

namespace TrackIT.DataAccess.Concrete
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string> //<- id için guid kullanacağız.
    {
        //Hangi database provider kullanacağımızı karar vermek için (program.cs) bir options parametresi göndereceğiz.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //<- base ile miras aldığımız sınıfın constructoruna parametre gönderiyoruz.
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductRegistiration> ProductRegistirations { get; set; }
        public DbSet<ProductRegistirationHistory> RegistirationHistorys { get; set; }
        public DbSet<ProductAsset> ProductAssets { get; set; }
    }

}
