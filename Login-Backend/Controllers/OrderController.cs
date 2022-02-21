using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login_Backend.Models;
using Login_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login_Backend.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly OrderServices _service;
        private readonly LoginService _serviceLogin;

        public OrderController(OrderServices service, LoginService serviceLogin)
        {
            _service = service;
            _serviceLogin = serviceLogin;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Order>> GetAll(int userId)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
                return await _service.GetAllOrders(userId);
            else
                throw new Exception("Token Expired Please login again before query ");
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<Order> GetById(int Id)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
                return await _service.GetOrderById(Id);
            else
                throw new Exception("Token Expired Please login again before query ");
        }
    }
}