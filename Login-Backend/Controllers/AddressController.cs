using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Login_Backend.Models;
using Login_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Login_Backend.Controllers
{
    [Route("Address")]
    public class AddressController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly AddressService _service;
        private readonly LoginService _serviceLogin;
        public AddressController(AddressService service, LoginService loginService)
        {
            _service = service;
            _serviceLogin = loginService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Address>> GetAddress(int userId) {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if(Logged)
            return await _service.GetAllAddress(userId);
            else
                throw new Exception("Token Expired Please login again ");
        }
        [HttpPost]
        [Route("Create")]
        public async Task<Address> CreateAddress(string zip, int numberHouse,string complement, int UserId)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
                string url = "https://viacep.com.br/ws/"+zip+"/json/";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream);
                string responseAddress = readStream.ReadToEnd();
                Address address = JsonConvert.DeserializeObject<Address>(responseAddress);
                address.UserId = UserId;
                address.NumberHouse = numberHouse;
                address.Complemento = complement;
                return await _service.CreateAddress(address);
            }               
            else
                throw new Exception("Token Expired Please login again ");
        }

        [HttpPost]
        [Route("Remove")]
        public async Task DeleteAddress(long id)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
                 await _service.DeleteAddress(id);
            else
                throw new Exception("Token Expired Please login again ");
        }

    }
}