using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.Business.Concrete;
using TrackIT.Business.Model;
using TrackIT.DataAccess.Abstract;
using TrackIT.DataAccess.Concrete;
using TrackIT.DataAccess.Entity;
using TrackIT.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(
    new ToastrOptions()
    {
        PositionClass = ToastPositions.TopRight,
        TimeOut = 3000,
    });

//Servisleri ekledeðimiz yer
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConn"));
});
//Identity Eklenmiþ Oluyor
//builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAutoMapper(typeof(Program));
//Dependency Inject için bir extension yazdýk.
builder.Services.DIContainer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
