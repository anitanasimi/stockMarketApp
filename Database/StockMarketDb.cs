using Microsoft.EntityFrameworkCore;
using StockMarketWithSignalR.Entities;

namespace StockMarketWithSignalR.Database
{
    public class StockMarketDb : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }

        public DbSet<MarketStatistic> MarketStatistics { get; set; }

        public DbSet<MarketState> MarketStates { get; set; }
        public StockMarketDb(DbContextOptions<StockMarketDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasData(new List<Currency>()
                {
                    new()
                    {
                        Coefficient = Convert.ToDecimal(1.5),
                        CurrencyCode = 1,
                        Title = "BitCoin",
                        Price = 60_000
                    },
                    new()
                    {
                        Coefficient = Convert.ToDecimal(1.1),
                        CurrencyCode = 2,
                        Title = "DogeCoin",
                        Price = 20
                    }
                });
            });

            modelBuilder.Entity<MarketStatistic>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.MarketStatistics)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_MarketStatistic_Currency");
            });

            modelBuilder.Entity<MarketState>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.Currency)
                    .WithOne(p => p.MarketState)
                    .HasForeignKey<MarketState>(d => d.CurrencyId)
                    .HasConstraintName("FK_MarketState_Currency");
            });
        }
    }
}
