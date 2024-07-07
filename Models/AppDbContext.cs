using Microsoft.EntityFrameworkCore;


namespace Product_Inventory_Management_System.Models
{
    public class AppDbContext :DbContext
    {
        DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
