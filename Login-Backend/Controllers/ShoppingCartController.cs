using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login_Backend.Models;
using Login_Backend.Models.ViewModels;
using Login_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login_Backend.Controllers
{
    [Route("Cart")]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _service;
        private readonly LoginService _serviceLogin;

        public ShoppingCartController(ShoppingCartService service, LoginService serviceLogin)
        {
            _service = service;
            _serviceLogin = serviceLogin;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("All")]
        public async Task<ShoppingCartViewModel> GetShopCart(int id)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
                return await _service.GetAllShoppingCart(id);
                 else
                throw new Exception("Token Expired Please login again before your query ");
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ShoppingCart> AddingShopCart(ShoppingCart shopping)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
                await _service.CreateShoppingCart(shopping);
                return shopping;
            }               
            else
                throw new Exception("Token Expired Please login again before Add ");
            
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<ShoppingCart> UpdateShop(int id, int amount)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
               ShoppingCart cart = await _service.UpdateShoppingCart(id, amount);
                return cart;
            }
            else
                throw new Exception("Token Expired Please login again before Add ");

        }

        [HttpPost]
        [Route("Remove")]
        public async Task RemoveShopCart(int Id)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
                await _service.RemoveShoppingCart(Id);
            else
                throw new Exception("Token Expired Please login again before Delete ");
        }


    }
}