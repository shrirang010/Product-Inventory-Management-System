using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Product_Inventory_Management_System.Interfaces;
using Product_Inventory_Management_System.Models;

namespace Product_Inventory_Management_System.Services
{
    public class Product_CRUD : IProduct_CRUD
    {
        private readonly AppDbContext context;
        public Product_CRUD(AppDbContext appDbcontext)
        {
            context = appDbcontext;
        }

        public async Task<Product> GetProductFromdbAsync(int id)
        {            
            var product = await context.Products.FindAsync(id);
            return product;
        }
        public FileResultDto GetAllProductsFromdbAsync()
        {
            var products = context.Products.FromSqlRaw("EXEC GetProductList").ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");                
                var properties = typeof(Product).GetProperties();
                
                for (int col = 0; col < properties.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = properties[col].Name;
                }
                
                for (int row = 0; row < products.Count; row++)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(products[row]);
                    }
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Products.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return new FileResultDto { FileContents = stream.ToArray(), ContentType = contentType, FileName = fileName };
            }
        }
        public async Task<bool> EditProductTodbAsync(int id, Product updatedproduct)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                product.Name = updatedproduct.Name;
                product.Price = updatedproduct.Price;
                product.Category = updatedproduct.Category;
                product.StockQuantity = updatedproduct.StockQuantity;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductTodbAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            context.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }

        public  void AddProductTodb(Product product)
        {
            context.Add(product);
            context.SaveChangesAsync();
            return;
        }
    }
}
