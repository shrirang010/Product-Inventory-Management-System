using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml.Style;
using Product_Inventory_Management_System.Models;
using Product_Inventory_Management_System.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Product_Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("/Products")]
    public class ProductController : Controller
    {
        
        private readonly IProduct_CRUD _product;
        private readonly ILogger<ProductController>_logger;
        public ProductController(IProduct_CRUD product_CRUD,ILogger<ProductController>logger)
        {            
            _product = product_CRUD;
            _logger = logger;
        }

        // ---- Get All Products ----------------------

        [HttpGet("")]
        public IActionResult GetProductList()
        {
            _logger.LogInformation("GET Product List Requested");
            try
            {
                FileResultDto file = _product.GetAllProductsFromdbAsync();
                _logger.LogInformation("GET Product List Successfull !");
                return File(file.FileContents, file.ContentType, file.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError("GET Product List Failed");
                return StatusCode(500, $"Internal Server Error : {ex.Message}");
            }
        }

        // ---- Get a Product by id --------------------

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            _logger.LogInformation("GET Product By Id Requested");
            try
            {
                Product product = await _product.GetProductFromdbAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Get Product By Id Successfull ! ");
                return Json(product);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GET Product By id Failed");
                return StatusCode(500, $"Internal Server Error :- {ex.Message}");
            }
        }

        // ----Add A new Product -----

        [HttpPost("Create")]
        public  IActionResult AddProduct([FromBody]Product product)
        {
            _logger.LogInformation("Create Product Requested");
            try
            {
                string Validation_error_message;
                if (product.Name.Length == 0)
                {
                    Validation_error_message = "Name attribute cannot be Empty";
                    return new ObjectResult(Validation_error_message) { StatusCode = 404 };
                }
                else if (product.Category.Length == 0)
                {
                    Validation_error_message = "Category attribute cannot be Empty";
                    return new ObjectResult(Validation_error_message) { StatusCode = 404 };
                }
                _product.AddProductTodb(product);
                _logger.LogInformation("Create Product Successfull");
                return Ok("Product Successfully Created");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Create Product Failed");
                return StatusCode(500, $"Internal Server Error :- {ex.Message}");

            }
        }

        // ----Edit A Product By id ------

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditProductAsync(int id,[FromBody]Product updatedproduct)
        {
            _logger.LogInformation("Edit Product Requested");
            try
            {
                bool value = await _product.EditProductTodbAsync(id, updatedproduct);
                if (value)
                {
                    _logger.LogInformation("Edit Product Successfull");
                    return Ok($"Succesfully Edited Product with id { id}");
                }
                return StatusCode(404);
            }
            catch (Exception ex) 
            {
                _logger.LogInformation("Edit Product Failed");
                return StatusCode(500, $"Internal Server Error :{ex.Message}");
            }
        }

        // ---- Delete A Product By id ------

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            _logger.LogInformation("Delete Product Requested");
            try
            {
                bool value = await _product.DeleteProductTodbAsync(id);
                if (value)
                {
                    _logger.LogInformation("Delete Product Successfull");
                    return Ok($"The Product with id {id} has been successfully Deleted");                    
                }
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Delete Product Failed");
                return StatusCode(500, $"Internal Server Error :{ex.Message}");
            }
        }

        // -------FRONTEND (RENDER VIEWS)----------

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
    }
}
