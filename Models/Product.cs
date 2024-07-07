using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_System.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Category{ get; set; } = "";
        public decimal Price { get; set; }  
        public int StockQuantity { get; set; }  


    }
}
