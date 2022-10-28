using Microsoft.EntityFrameworkCore;
using HiveManager.Models;

namespace HiveManager.Data;
public class HiveContext : DbContext
{
    public DbSet<Hive> Hives { get; set; }
    public DbSet<BeeSpecie> BeeSpecies { get; set; }
    public DbSet<WithdrawndHoney> WithdrawndsHoney { get; set; }
    public DbSet<Derivative> Derivatives { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Price> Prices { get; set; }

    public HiveContext(DbContextOptions<HiveContext> options)
    : base(options)
    {
    }


}
