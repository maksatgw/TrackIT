﻿using Microsoft.Extensions.FileProviders;
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

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

            services.AddScoped<IProductDataAccess, EFProduct>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<ICategoryDataAccess, EFCategory>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<IProductRegisterDataAccess, EFProductRegister>();
            services.AddScoped<IProductRegisterService, ProductRegisterManager>();

            services.AddScoped<IProductRegisterHistoryDataAccess, EFProductRegisterHistory>();
            services.AddScoped<IProductRegisterHistoryService, ProductRegisterHistoryManager>();

            services.AddScoped<IProductAssetDataAccess, EFProductAsset>();
            services.AddScoped<IProductAssetService, ProductAssetManager>();

            services.AddScoped<ILocationDataAccess, EFLocation>();
            services.AddScoped<ILocationService, LocationManager>();

        }
    }
}
