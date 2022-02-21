using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login_Backend.Models;
using Login_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login_Backend.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ProductService _service;
        private readonly LoginService _serviceLogin;
        public ProductController(ProductService service, LoginService loginService)
        {
            _service = service;
            _serviceLogin = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<List<Product>> GetAllProduct()
        {
           bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
                return await _service.GetAllProducts();
            else
                throw new Exception("Token Expired Please login again before query ");
        }
        [HttpPost]
        [Route("Create")]
        public async Task<Product> CreateProduct(Product product)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
                await _service.CreatePoduct(product);
                return product;
            }                
            else
                throw new Exception("Token Expired Please do login again before Create product ");
            
            
        }
        [HttpPut]
        [Route("Edit")]
        public async Task<Product> EditProduct(Product product)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
                await _service.UpdatePoduct(product);
                return product;
            }
            else
                throw new Exception("Token Expired Please do login again before update product");           
        }

        [HttpPost]
        [Route("Remove")]
        public async Task RemoveProduct(int Id)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
                await _service.RemoveProduct(Id);               
            }
            else
                throw new Exception("Token Expired Please do login again before delete product");
           
        }

    }
}