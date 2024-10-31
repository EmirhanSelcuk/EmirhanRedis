using Microsoft.EntityFrameworkCore;

namespace EmirhanRedis.API.Model
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(
                new Product() {Id=1, Name="Pen 1" },
                new Product() {Id=2, Name="Pen 2" },
                new Product() {Id=3, Name="Pen 3" }
                );





            base.OnModelCreating(modelBuilder);
        }
    }
}
