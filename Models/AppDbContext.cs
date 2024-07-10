using Microsoft.EntityFrameworkCore;


namespace Product_Inventory_Management_System.Models
{
    public class AppDbContext :DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext>Options) :base(Options)
        { 

        }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
     
    }
}
