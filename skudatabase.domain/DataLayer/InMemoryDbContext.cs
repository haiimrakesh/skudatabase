using Microsoft.EntityFrameworkCore;
using skudatabase.domain.Infrastructure;
using skudatabase.domain.Models;

namespace skudatabase.domain.DataLayer
{
    public class InMemoryDbContext : DbContext, ISKUDbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options)
        {
            SKUs = Set<SKU>();
            SKUPartValues = Set<SKUPartValues>();
            SKUConfigs = Set<SKUConfig>();
            SKUConfigSequences = Set<SKUConfigSequence>();
            SKUPartConfigs = Set<SKUPartConfig>();

        }

        public DbSet<SKU> SKUs { get; set; }
        public DbSet<SKUPartValues> SKUPartValues { get; set; }
        public DbSet<SKUConfig> SKUConfigs { get; set; }
        public DbSet<SKUConfigSequence> SKUConfigSequences { get; set; }
        public DbSet<SKUPartConfig> SKUPartConfigs { get; set; }

        IQueryable<SKU> ISKUDbContext.SKUs => SKUs;

        IQueryable<SKUPartConfig> ISKUDbContext.SKUPartConfigs => SKUPartConfigs;

        IQueryable<SKUPartValues> ISKUDbContext.SKUPartValues => SKUPartValues;

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