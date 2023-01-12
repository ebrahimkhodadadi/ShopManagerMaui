
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TransportLogger>()
        .HasOne(x => x.SourceStore)
        .WithMany()
        .HasForeignKey(x => x.SourceStoreId)
        .OnDelete(DeleteBehavior.ClientSetNull); 
        
        modelBuilder.Entity<TransportLogger>()
        .HasOne(x => x.DestinationStore)
        .WithMany()
        .HasForeignKey(x => x.DestinationStoreId)
        .OnDelete(DeleteBehavior.ClientSetNull);
    }


    DbSet<ProductDetails> ProductDetails { get; set; }
    DbSet<Products> Products { get; set; }
    DbSet<StoredProducts> StoredProducts { get; set; }
    DbSet<Stores> Stores { get; set; }
    DbSet<TransportLogger> TransportLogger { get; set; }
    DbSet<Users> Users { get; set; }
    DbSet<Basket> Baskets { get; set; }
    DbSet<TransferLoggerDetail> TransferLoggerDetail { get; set; }
}