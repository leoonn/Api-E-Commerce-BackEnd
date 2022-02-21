using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login_Backend.Dtos;
using Login_Backend.Models;
using Login_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login_Backend.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerservice;
        public RegisterController(RegisterService registerservice)
        {
            _registerservice = registerservice;
        }

        [HttpPost]
        [Route("Register")]        
        public async Task<UserDtos> RegisterAsync(User user)
        {
            if (ModelState.IsValid)
            {               
                UserDtos userdto=  await _registerservice.CreateNewUserAsync(user);
                return userdto;
            }
            else {
                return null;
            }            
            
        }
    }
}