using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using skudatabase.domain.Infrastructure.Repositories;
using skudatabase.domain.Models;

namespace skudatabase.MVC.Web.Database;

public class SKUDatabaseContext : DbContext, ISKUDbContext
{

    public SKUDatabaseContext(DbContextOptions<SKUDatabaseContext> options)
    : base(options)
    {
    }

    public DbSet<SKU> SKUs { get; set; }
    public DbSet<SKUPartConfig> SKUPartConfigs { get; set; }
    public DbSet<SKUPartValues> SKUPartValues { get; set; }
    public DbSet<SKUConfig> SKUConfigs { get; set; }
    public DbSet<SKUConfigSequence> SKUConfigSequences { get; set; }

    IQueryable<SKUPartConfig> ISKUDbContext.SKUPartConfigs => SKUPartConfigs.AsQueryable();
    IQueryable<SKUPartValues> ISKUDbContext.SKUPartValues => SKUPartValues.AsQueryable();
    IQueryable<SKUConfig> ISKUDbContext.SKUConfigs => SKUConfigs.AsQueryable();
    IQueryable<SKUConfigSequence> ISKUDbContext.SKUConfigSequences => SKUConfigSequences.AsQueryable();
    IQueryable<SKU> ISKUDbContext.SKUs => SKUs.AsQueryable();

    public DbSet<T>? GetDbSet<T>() where T : class
    {
        if (typeof(T) == typeof(SKU))
            return SKUs as DbSet<T>;
        if (typeof(T) == typeof(SKUPartConfig))
            return SKUPartConfigs as DbSet<T>;
        if (typeof(T) == typeof(SKUPartValues))
            return SKUPartValues as DbSet<T>;
        if (typeof(T) == typeof(SKUConfig))
            return SKUConfigs as DbSet<T>;
        if (typeof(T) == typeof(SKUConfigSequence))
            return SKUConfigSequences as DbSet<T>;

        throw new ArgumentException("Invalid type");
    }

}
