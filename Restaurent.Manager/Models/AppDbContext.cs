using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillRecord> BillRecord { get; set; }

        public AppDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=RestaurentManager;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BillRecord>().HasKey(x => new { x.BillId, x.FoodId });
        }
    }
}
