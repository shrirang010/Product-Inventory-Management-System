using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml.Style;
using Product_Inventory_Management_System.Models;
using Product_Inventory_Management_System.Interfaces;

namespace Product_Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("/Products")]
    public class ProductController : Controller
    {
        
        private readonly IProduct_CRUD _product;
        public ProductController(IProduct_CRUD product_CRUD)
        {
            
            _product = product_CRUD;
        }

        [HttpGet("")]
        public IActionResult GetProductList()
        {
            FileResultDto file = _product.ReadAllProductsFromdb();
            return File(file.FileContents,file.ContentType,file.FileName);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            Product p = await _product.GetProduct(id);
            if (p == null)
            {
                return NotFound();
            }
            return Json(p);
        }
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost("Create")]
        public  IActionResult AddProduct([FromBody]Product product)
        {
            _product.AddProductTodb(product);
            return StatusCode(200,Content("Product Successfully Created","application/text"));
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditProduct(int id,[FromBody]Product updatedproduct)
        {
            bool value = await _product.EditProductTodb(id,updatedproduct);
            if (value)
            {
                return StatusCode(200, Content($"Succesfully Edited Product with id {id}", "application/text"));
            }
            return StatusCode(404);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool value = await _product.DeleteProductTodb(id);
            if(value)
            {
                return StatusCode(200,Content($"The Product with id {id} has been successfully Deleted","application/text"));
            }
            return StatusCode(404);
        }
    }
}
