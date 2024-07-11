using Microsoft.AspNetCore.Mvc;
using Product_Inventory_Management_System.Models;

namespace Product_Inventory_Management_System.Interfaces
{
    public interface IProduct_CRUD
    {
        public FileResultDto ReadAllProductsFromdb();
        public Task<Product> GetProduct(int id);
        public Task<bool> EditProductTodb(int id,  Product updatedproduct);
        public Task<bool> DeleteProductTodb(int id);
        public void AddProductTodb(Product product);


    }
}
