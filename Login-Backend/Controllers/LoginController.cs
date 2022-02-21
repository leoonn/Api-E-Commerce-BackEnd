using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login_Backend.Models;
using Login_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using Login_Backend.Tools;

namespace Login_Backend.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _service;
        private Cookies cookies= new Cookies();

        public LoginController(LoginService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<string> LoginUserAsync(string email, string password, bool keepConnected)
        {
            string response = await _service.Login(email, password);
            if (response != "" && keepConnected)
            {
                cookies.CreateCookie("Session", response, Response, 2);
            }            
            else
                cookies.CreateCookie("Session", response, Response, 0);

            return response;           
        }

        [HttpPost]
        [Route("Check")]
        public async Task<string> CheckSession()
        {
           bool check = await _service.CheckIsLogged(Request);
            if (check)
            {
                return check.ToString() + " You are loggedin";
            }
            else {                          
                return check.ToString() + " You aren't loggedin";
            }
                
            
        }

    }
}