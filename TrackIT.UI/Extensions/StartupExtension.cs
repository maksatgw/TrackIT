using TrackIT.Business.Abstract;
using TrackIT.Business.Concrete;
using TrackIT.Business.Model;
using TrackIT.DataAccess.Abstract;
using TrackIT.DataAccess.Concrete;
using TrackIT.DataAccess.Entity;

namespace TrackIT.UI.Extensions
{
    public static class StartupExtension
    {
        public static void DIContainer(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IProductDataAccess, EFProduct>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<ICategoryDataAccess, EFCategory>();
            services.AddScoped<ICategoryService, CategoryManager>();
        }
    }
}
