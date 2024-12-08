using Microsoft.EntityFrameworkCore;

namespace WorldCountriesDirectoryApiApp.Storage
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<DbCountry> Countries { get; set; } // Таблица стран

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string useConnection = configuration.GetSection("UseConnection").Value ?? "DefaultConnection";
            string? connectionString = configuration.GetConnectionString(useConnection);
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация индекса для IsoAlpha2
            modelBuilder.Entity<DbCountry>()
                .HasIndex(c => c.IsoAlpha2)
                .IsUnique();
        }
    }
}
