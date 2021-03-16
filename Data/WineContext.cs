using System;
using System.Threading;
using System.Threading.Tasks;
using backend_labo01_wijn.Config;
using backend_labo01_wijn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace backend_labo01_wijn.Data
{
    public interface IWineContext
    {
        DbSet<Wine> Wines { get; set; }
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class WineContext : DbContext, IWineContext
    {
        public DbSet<Wine> Wines { get; set; }

        private ConnectionStrings _connectionStrings;

        public WineContext(DbContextOptions<WineContext> options, IOptions<ConnectionStrings> connectionStrings) : base(options)
        {
            _connectionStrings = connectionStrings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            options.UseSqlServer(_connectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wine>().HasData(new Wine() { WineId = Guid.NewGuid(), Name = "Terrases de la mer", Year = 2018, Country = "FR", Color = "rosé", Price = 19.95, Grapes = "bessen" });
        }
    }
}
