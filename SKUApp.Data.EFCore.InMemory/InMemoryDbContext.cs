using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore.InMemory
{
    public class InMemoryDbContext : DbContext, ISKUDbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options)
        {
            SKUs = Set<SKU>();
            SKUPartEntries = Set<SKUPartEntry>();
            SKUConfigs = Set<SKUConfig>();
            SKUConfigSequences = Set<SKUConfigSequence>();
            SKUPartConfigs = Set<SKUPartConfig>();

        }

        public DbSet<T>? GetDbSet<T>() where T : class
        {
            if (typeof(T) == typeof(SKU))
                return SKUs as DbSet<T>;
            if (typeof(T) == typeof(SKUPartConfig))
                return SKUPartConfigs as DbSet<T>;
            if (typeof(T) == typeof(SKUPartEntry))
                return SKUPartEntries as DbSet<T>;
            if (typeof(T) == typeof(SKUConfig))
                return SKUConfigs as DbSet<T>;
            if (typeof(T) == typeof(SKUConfigSequence))
                return SKUConfigSequences as DbSet<T>;

            throw new ArgumentException("Invalid type");
        }

        public DbSet<SKU> SKUs { get; set; }
        public DbSet<SKUPartEntry> SKUPartEntries { get; set; }
        public DbSet<SKUConfig> SKUConfigs { get; set; }
        public DbSet<SKUConfigSequence> SKUConfigSequences { get; set; }
        public DbSet<SKUPartConfig> SKUPartConfigs { get; set; }

        IQueryable<SKU> ISKUDbContext.SKUs => SKUs;

        IQueryable<SKUPartConfig> ISKUDbContext.SKUPartConfigs => SKUPartConfigs;

        IQueryable<SKUPartEntry> ISKUDbContext.SKUPartEntries => SKUPartEntries;

        IQueryable<SKUConfig> ISKUDbContext.SKUConfigs => SKUConfigs;

        IQueryable<SKUConfigSequence> ISKUDbContext.SKUConfigSequences => SKUConfigSequences;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryDb");
            }
        }

        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entity relationships and constraints here
        }

    }
}