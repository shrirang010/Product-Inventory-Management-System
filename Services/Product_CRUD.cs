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

        public async Task<Product> GetProduct(int id)
        {            
            var p = await context.Products.FindAsync(id);
            return p;
        }
        public FileResultDto ReadAllProductsFromdb()
        {
            var products = context.Products.FromSqlRaw("EXEC GetProductList").ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            // Create an Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet

                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add header row
                worksheet.Cells[1, 1].Value = "ProductID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Category";
                worksheet.Cells[1, 4].Value = "Price";
                worksheet.Cells[1, 5].Value = "StockQuantity";

                // Add data rows
                for (int i = 0; i < products.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = products[i].ProductID;
                    worksheet.Cells[i + 2, 2].Value = products[i].Name;
                    worksheet.Cells[i + 2, 3].Value = products[i].Category;
                    worksheet.Cells[i + 2, 4].Value = products[i].Price;
                    worksheet.Cells[i + 2, 5].Value = products[i].StockQuantity;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Products.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return new FileResultDto { FileContents = stream.ToArray(), ContentType = contentType, FileName = fileName };
            }
        }
        public async Task<bool> EditProductTodb(int id, Product updatedproduct)
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

        public async Task<bool> DeleteProductTodb(int id)
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
