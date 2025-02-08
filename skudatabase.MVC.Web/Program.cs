using Microsoft.EntityFrameworkCore;
using skudatabase.domain.Infrastructure.Repositories;
using skudatabase.domain.Infrastructure.Services;
using skudatabase.domain.Infrastructure.UnitOfWork;
using skudatabase.domain.Services;
using skudatabase.MVC.Web.Database;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ISKUDbContext, SKUDatabaseContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SkuDbContext")));
        builder.Services.AddScoped<ISKUPartConfigService, SKUPartConfigService>();
        builder.Services.AddScoped<ISKUUnitOfWork, SKUSqliteUnitOfWork>();
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}