using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CachingMVC.Models;
using CachingMVC.Services;
using CachingMVC.Repositories;

namespace CachingMVC.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository _productRepository;
        CashService _cashService;
        public HomeController(ProductRepository productRepository,CashService cashService)
        {
            _productRepository = productRepository;
            productRepository.Initialize();
            _cashService = cashService;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {

            var product = await _cashService.TryExecuteAsync<Product>(() => _productRepository.GetProduct(id),id);

            if (product != null)
                return Content($"Product: {product.Name}");
            return Content("Product not found");
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            Product productCreate = new Product();
            //productCreate.Name = "Nokia 320";
            //productCreate.Price = 9324;
            //productCreate.Company = "Nokia";

            var product = await _cashService.TryExecuteAsync<Product>(() => _productRepository.AddProduct(productCreate), null);

            if (product != null)
                return Content($"Product: {product.Name} is created with id: {product.Id}!");
            return Content("Product not found");
        }
    }
}
