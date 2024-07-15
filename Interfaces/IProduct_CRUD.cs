using Microsoft.AspNetCore.Mvc;
using Product_Inventory_Management_System.Models;

namespace Product_Inventory_Management_System.Interfaces
{
    public interface IProduct_CRUD
    {
        public FileResultDto GetAllProductsFromdbAsync();
        public Task<Product> GetProductFromdbAsync(int id);
        public Task<bool> EditProductTodbAsync(int id,  Product updatedproduct);
        public Task<bool> DeleteProductTodbAsync(int id);
        public void AddProductTodb(Product product);


    }
}
