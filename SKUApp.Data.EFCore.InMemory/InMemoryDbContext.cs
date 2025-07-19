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

        public DbSet<SKU> SKUs { get; set; }
        public DbSet<SKUPartEntry> SKUPartEntries { get; set; }
        public DbSet<SKUConfig> SKUConfigs { get; set; }
        public DbSet<SKUConfigSequence> SKUConfigSequences { get; set; }
        public DbSet<SKUPartConfig> SKUPartConfigs { get; set; }

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